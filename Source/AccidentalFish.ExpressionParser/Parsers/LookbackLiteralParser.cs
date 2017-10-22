using System;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public class LookbackLiteralParser : IParser
    {
        private readonly string _literal;
        private readonly Func<ExpressionNode, bool> _isValidLookback;

        private readonly Func<string, ExpressionNode> _factory;

        public LookbackLiteralParser(string literal, Func<ExpressionNode, bool> isValidLookback, Func<string, ExpressionNode> factory)
        {
            _literal = literal;
            _isValidLookback = isValidLookback;
            _factory = factory;
        }

        public bool IsPartialMatch(string partialToken, ExpressionNode last)
        {
            if (partialToken.Length > _literal.Length) return false;

            return _literal.StartsWith(partialToken) && _isValidLookback(last);
        }

        public bool IsCompleteMatch(string token)
        {
            return _literal.Equals(token);
        }

        public Func<string, ExpressionNode> Factory => _factory;
    }
}
