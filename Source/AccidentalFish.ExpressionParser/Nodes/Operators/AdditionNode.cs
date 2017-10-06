namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class AdditionNode : BinaryOperatorNode
    {
        public const string Literal = "+";

        public AdditionNode() : base(AssociativityEnum.Left, PrecedenceEnum.Additive)
        {
        }

        public AdditionNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Additive, left, right)
        {
        }
    }
}
