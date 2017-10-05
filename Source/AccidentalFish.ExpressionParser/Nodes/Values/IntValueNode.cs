using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Values
{
    public class IntValueNode : ValueNode
    {
        public IntValueNode()
        {

        }

        public IntValueNode(string expressionToken)
        {
            Value = int.Parse(expressionToken);
        }

        public IntValueNode(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}
