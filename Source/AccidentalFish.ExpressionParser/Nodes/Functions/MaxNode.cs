namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class MaxNode : FunctionNode
    {
        public const string Literal = "max";

        public MaxNode()
        {
        }

        public MaxNode(ExpressionNode[] parameters) : base(parameters)
        {
        }

        public override int NumberOfParameters()
        {
            return 2;
        }
    }
}
