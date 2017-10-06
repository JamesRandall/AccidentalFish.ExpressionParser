namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class DivisionNode : BinaryOperatorNode
    {
        public const string Literal = "/";

        public DivisionNode() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public DivisionNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
