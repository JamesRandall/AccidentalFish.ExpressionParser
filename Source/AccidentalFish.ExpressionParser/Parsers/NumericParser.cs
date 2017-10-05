using System;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public class NumericParser : IParser
    {
        private readonly Func<string,ExpressionNode> _factory;

        public NumericParser(Func<string,ExpressionNode> factory)
        {
            _factory = factory;
        }

        public bool IsPartialMatch(string partialToken, ExpressionNode last)
        {
            bool result = double.TryParse(partialToken, out double _);
            if (result && (partialToken.StartsWith("+") || partialToken.StartsWith("-")))
            {
                result = false;
            }
            return result;
        }

        public bool IsCompleteMatch(string token)
        {
            return double.TryParse(token, out double _);
        }

        public Func<string,ExpressionNode> Factory => _factory;
    }
}
