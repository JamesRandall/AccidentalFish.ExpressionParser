using System;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public class FunctionParser : IParser
    {
        private readonly Func<string, ExpressionNode> _factory;

        public FunctionParser(Func<string, ExpressionNode> factory)
        {
            _factory = factory;
        }

        public bool IsPartialMatch(string partialToken, ExpressionNode last)
        {
            throw new NotImplementedException();
        }

        public bool IsCompleteMatch(string token)
        {
            throw new NotImplementedException();
        }

        public Func<string, ExpressionNode> Factory => _factory;
    }
}
