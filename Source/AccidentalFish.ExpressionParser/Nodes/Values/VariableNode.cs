using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Values
{
    public class VariableNode : ValueNode
    {
        public VariableNode()
        {
            
        }

        public VariableNode(string expressionToken)
        {
            Name = expressionToken;
        }

        public string Name { get; }
    }
}
