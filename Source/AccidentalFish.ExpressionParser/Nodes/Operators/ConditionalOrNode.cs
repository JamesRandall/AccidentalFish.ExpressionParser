namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class ConditionalOrNode : BinaryOperatorNode
    {
        public const string Literal = "||";

        public ConditionalOrNode() : base(AssociativityEnum.Left, PrecedenceEnum.Relational)
        {
        }

        public ConditionalOrNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Relational, left, right)
        {
        }
    }
}
