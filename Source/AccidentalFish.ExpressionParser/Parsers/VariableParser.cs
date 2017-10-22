using System;
using System.Text.RegularExpressions;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public class VariableParser : IParser
    {
        private readonly Func<string, ExpressionNode> _factory;
        private readonly Regex _test = new Regex(@"\@{1}[a-z]{0,}[a-z]$");

        public VariableParser(Func<string, ExpressionNode> factory)
        {
            _factory = factory;
        }

        // A variable begins with @ but then has a-z
        public bool IsPartialMatch(string partialToken, ExpressionNode last)
        {
            if (partialToken.Length == 1)
            {
                return partialToken.Substring(0, 1) == "@";
            }
            bool result = _test.IsMatch(partialToken);
            return result;
        }

        public bool IsCompleteMatch(string token)
        {
            bool result = _test.IsMatch(token);
            return result;
        }

        public Func<string, ExpressionNode> Factory => _factory;
    }
}
