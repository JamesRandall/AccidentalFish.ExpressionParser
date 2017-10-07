using System.Collections.Generic;

namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public abstract class FunctionNode : ExpressionNode
    {
        protected FunctionNode()
        {

        }

        protected FunctionNode(ExpressionNode[] parameters)
        {
            Parameters = parameters;
        }

        public abstract int NumberOfParameters();

        public IReadOnlyCollection<ExpressionNode> Parameters { get; internal set; }
    }
}
