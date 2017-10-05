using System;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Parsers
{
    internal class SimpleLiteralParser : IParser
    {
        private readonly string _literal;

        private readonly Func<string,ExpressionNode> _factory;

        public SimpleLiteralParser(string literal, Func<string,ExpressionNode> factory)
        {
            _literal = literal;
            _factory = factory;
        }

        public bool IsPartialMatch(string partialToken, ExpressionNode last)
        {
            if (partialToken.Length > _literal.Length) return false;

            return _literal.StartsWith(partialToken);
        }

        public bool IsCompleteMatch(string token)
        {
            return _literal.Equals(token);
        }

        public Func<string,ExpressionNode> Factory => _factory;
    }
}
