namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class NegateNode : UnaryOperatorNode
    {
        public const string Literal = "-";

        public NegateNode() : base(AssociativityEnum.Right, PrecedenceEnum.Unary)
        {
        }

        public NegateNode(AssociativityEnum associativity, PrecedenceEnum precedence, ExpressionNode associatedNode) : base(associativity, precedence, associatedNode)
        {
        }
    }
}
