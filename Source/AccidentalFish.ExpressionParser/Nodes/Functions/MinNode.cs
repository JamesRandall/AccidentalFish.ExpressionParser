namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class MinNode : VariableParamsFunctionNode
    {
        public const string Literal = "max";

        public MinNode()
        {
        }

        public MinNode(ExpressionNode[] parameters) : base(parameters)
        {
        }
    }
}
