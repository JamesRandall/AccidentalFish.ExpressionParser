namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class LengthNode : FunctionNode
    {
        public const string Literal = "length";

        public LengthNode()
        {
        }

        public LengthNode(ExpressionNode[] parameters) : base(parameters)
        {
        }

        public override int NumberOfParameters()
        {
            return 1;
        }
    }
}
