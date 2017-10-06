namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class MultiplicationNode : BinaryOperatorNode
    {
        public const string Literal = "*";

        public MultiplicationNode() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public MultiplicationNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
