using System;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public class FunctionParser : IParser
    {
        private readonly string _functionName;
        private readonly Func<string, ExpressionNode> _factory;
        
        public FunctionParser(string functionName, Func<string, ExpressionNode> factory)
        {
            _functionName = functionName;
            _factory = factory;
        }

        public bool IsPartialMatch(string partialToken, ExpressionNode last)
        {
            return _functionName.StartsWith(partialToken);
        }

        public bool IsCompleteMatch(string token)
        {
            return _functionName.Equals(token);
        }

        public Func<string, ExpressionNode> Factory => _factory;
    }
}
