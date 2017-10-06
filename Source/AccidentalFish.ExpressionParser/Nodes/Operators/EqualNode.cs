namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class EqualNode : BinaryOperatorNode
    {
        public const string Literal = "==";

        public EqualNode() : base(AssociativityEnum.Left, PrecedenceEnum.Equality)
        {
        }

        public EqualNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Equality, left, right)
        {

        }
    }
}
