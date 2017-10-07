namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class NegateNode : UnaryOperatorNode
    {
        public const string Literal = "-";

        public NegateNode() : base(AssociativityEnum.Right, PrecedenceEnum.Unary)
        {
        }

        public NegateNode(ExpressionNode associatedNode) : base(AssociativityEnum.Right, PrecedenceEnum.Unary, associatedNode)
        {
        }
    }
}
