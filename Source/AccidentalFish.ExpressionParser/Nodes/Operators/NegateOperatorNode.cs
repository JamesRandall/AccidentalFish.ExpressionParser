namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class NegateOperatorNode : UnaryOperatorNode
    {
        public const string Literal = "-";

        public NegateOperatorNode() : base(AssociativityEnum.Right, PrecedenceEnum.Unary)
        {
        }

        public NegateOperatorNode(AssociativityEnum associativity, PrecedenceEnum precedence, ExpressionNode associatedNode) : base(associativity, precedence, associatedNode)
        {
        }
    }
}
