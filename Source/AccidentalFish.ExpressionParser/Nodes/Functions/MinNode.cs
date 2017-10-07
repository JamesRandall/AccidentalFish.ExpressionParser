namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class MinNode : VariableParamsFunctionNode
    {
        public const string Literal = "min";

        public MinNode()
        {
        }

        public MinNode(ExpressionNode[] parameters) : base(parameters)
        {
        }
    }
}
