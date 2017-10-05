using System;

namespace AccidentalFish.ExpressionParser.Linq.Exceptions
{
    public class UnsupportedExpressionNodeTypeException : System.Exception
    {
        public UnsupportedExpressionNodeTypeException(string message, Type nodeType) : base(message)
        {
            NodeType = nodeType;
        }

        public Type NodeType { get; }
    }
}
