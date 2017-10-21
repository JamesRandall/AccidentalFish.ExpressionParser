using System;
using System.Collections.Generic;
using System.Linq;
using AccidentalFish.ExpressionParser.Exception;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Values;
using AccidentalFish.ExpressionParser.Parsers;

namespace AccidentalFish.ExpressionParser
{
    public sealed class ExpressionSplitter : IExpressionSplitter
    {
        private readonly IParserProvider _parserProvider;
        private readonly Options _options;

        public ExpressionSplitter(IParserProvider parserProvider, Options options = null)
        {
            _parserProvider = parserProvider;
            if (options == null)
            {
                options = Options.Default;
            }
            _options = options;
        }

        public IReadOnlyCollection<ExpressionNode> Split(string expression)
        {
            // The aim of expression splitting is to take the string and convert it into a set of expression nodes ordered as they
            // are in the string.
            // The shunting yard algorithm can then be run on this to create an RPN expression.
            // expression splitting is handled as follows:
            //  1. for each scan of the string
            //      a. Build a copy of the list of installed parsers
            //      b. Move forward through the string a character at a time (ignoring whitespace) and removing parsers that don't match abnd keeping
            //         track of the last positive matches
            //      c. When no parsers are left look at the last set of positive matches and it should contain a single parser - if so invoke the factory
            //         and add the resulting node to the list of found nodes. If there is more than one match then the expression syntax has been set up
            //         in an ambigous fashion or the expression is faulty so throw an error
            //      d. Backtrack one character and repeat the process scanning the string again until the end of the string is reached

            List<ExpressionNode> result = new List<ExpressionNode>();
            IReadOnlyCollection<IParser> possibleParsers = new List<IParser>();
            string remainingExpression = expression;
            int currentLength = 0;
            ExpressionNode lastNode = null;
            string partialToken = "";
            while (remainingExpression.Length > 0)
            {
                ExpressionNode newNode;
                if (currentLength == 0)
                {
                    partialToken = "";
                    possibleParsers = _parserProvider.Get().ToList();
                }

                currentLength = ExtractNextNoneWhitespaceCharacter(remainingExpression, currentLength, ref partialToken);
                if (partialToken.Length == 0)
                {
                    // we ended on whitespace
                    break;
                }

                if (partialToken.Length == 1 && partialToken == "\"")
                {
                    (remainingExpression, newNode) = ExtractString(remainingExpression);
                    currentLength = 0;
                }
                else
                {
                    (possibleParsers, remainingExpression, currentLength, newNode) =
                        AttemptParserExtract(possibleParsers, partialToken, lastNode, remainingExpression, currentLength);
                }
                

                if (newNode != null)
                {
                    result.Add(newNode);
                }

                lastNode = result.LastOrDefault();
            }

            return result;
        }

        private static (IReadOnlyCollection<IParser>, string, int, ExpressionNode) AttemptParserExtract(IReadOnlyCollection<IParser> possibleParsers, string partialToken,
            ExpressionNode lastNode, string remainingExpression, int currentLength)
        {
            IReadOnlyCollection<IParser> stillValidParsers = possibleParsers.Where(x => x.IsPartialMatch(partialToken, lastNode)).ToArray();
            ExpressionNode newNode = null;
            if (stillValidParsers.Count == 0)
            {
                IReadOnlyCollection<IParser> remainingParsersAfterCompleteMatch = possibleParsers.Where(x => x.IsCompleteMatch(partialToken.Substring(0, partialToken.Length - 1))).ToArray();
                if (remainingParsersAfterCompleteMatch.Count > 1)
                {
                    throw new ExpressionParseException(
                        $"Syntax error. Expression component {remainingExpression} is ambiguous.");
                }
                newNode = remainingParsersAfterCompleteMatch.Single().Factory(partialToken.Substring(0, partialToken.Length - 1));
                remainingExpression = remainingExpression.Substring(currentLength - 1);
                currentLength = 0;
            }

            possibleParsers = stillValidParsers;

            // end of string
            if (currentLength >= remainingExpression.Length)
            {
                if (possibleParsers.Count > 1)
                {
                    throw new ExpressionParseException($"Syntax error. Expression component {remainingExpression} is ambiguous.");
                }
                newNode = possibleParsers.Single().Factory(remainingExpression);
                remainingExpression = "";
            }
            return (possibleParsers, remainingExpression, currentLength, newNode);
        }

        private (string, ExpressionNode) ExtractString(string remainingExpression)
        {
            remainingExpression = remainingExpression.Substring(1);
            int nextQuoteIndex = 0;
            do
            {
                nextQuoteIndex = remainingExpression.IndexOf("\"", nextQuoteIndex, StringComparison.Ordinal);
                if (nextQuoteIndex == 0 || remainingExpression.Substring(nextQuoteIndex - 1, 1) != "\\")
                {
                    string value = remainingExpression.Substring(0, nextQuoteIndex);
                    remainingExpression = remainingExpression.Substring(nextQuoteIndex + 1);
                    return (remainingExpression, new StringValueNode(value));
                }
                nextQuoteIndex++;
            } while (nextQuoteIndex != -1);
            throw new ExpressionParseException("Malformed string in expression");
        }

        private int ExtractNextNoneWhitespaceCharacter(string remainingExpression, int currentLength, ref string partialToken)
        {
            // we can't just do a global replace on whitespace characters to remove them as some operators / syntax
            // can change their meaning. For example string literals (not supported yet but will be).
            string nextCharacter;
            do
            {
                nextCharacter = remainingExpression.Substring(currentLength, 1);
                currentLength++;
            } while (IsWhitespace(nextCharacter) && currentLength < remainingExpression.Length);
            if (!IsWhitespace(nextCharacter))
            {
                partialToken = partialToken + nextCharacter;
            }
            return currentLength;
        }

        private bool IsWhitespace(string character)
        {
            return _options.WhitespaceCharacters.Contains(character);
        }
    }
}
