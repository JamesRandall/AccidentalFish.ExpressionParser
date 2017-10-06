namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class PowerOperatorNode : BinaryOperatorNode
    {
        public const string Literal = "^";

        public PowerOperatorNode() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public PowerOperatorNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
