using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Values
{
    public class DoubleValueNode : ValueNode
    {
        public DoubleValueNode()
        {
            
        }

        public DoubleValueNode(string expressionToken)
        {
            Value = double.Parse(expressionToken);
        }

        public DoubleValueNode(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
