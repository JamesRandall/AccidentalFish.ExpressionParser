namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class MinNode : FunctionNode
    {
        public const string Literal = "min";

        public MinNode()
        {
        }

        public MinNode(ExpressionNode[] parameters) : base(parameters)
        {
        }

        public override int NumberOfParameters()
        {
            return 2;
        }
    }
}
