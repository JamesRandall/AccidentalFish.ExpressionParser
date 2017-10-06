namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class SubtractionNode : BinaryOperatorNode
    {
        public const string Literal = "-";

        public SubtractionNode() : base(AssociativityEnum.Left, PrecedenceEnum.Additive)
        {
        }

        public SubtractionNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Additive, left, right)
        {
        }
    }
}
