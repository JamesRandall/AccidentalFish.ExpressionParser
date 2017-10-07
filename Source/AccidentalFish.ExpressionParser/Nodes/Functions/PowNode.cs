namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class PowNode : FunctionNode
    {
        public const string Literal = "pow";

        public PowNode()
        {
        }

        public PowNode(ExpressionNode[] parameters) : base(parameters)
        {
            
        }

        public override int NumberOfParameters()
        {
            return 2;
        }
    }
}
