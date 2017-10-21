namespace AccidentalFish.ExpressionParser.Nodes.Values
{
    public class StringValueNode : ValueNode
    {
        public StringValueNode()
        {

        }

        public StringValueNode(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
