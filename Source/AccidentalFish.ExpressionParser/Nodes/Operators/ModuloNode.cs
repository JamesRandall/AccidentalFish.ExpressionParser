namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class ModuloNode : BinaryOperatorNode
    {
        public const string Literal = "%";

        public ModuloNode() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public ModuloNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
