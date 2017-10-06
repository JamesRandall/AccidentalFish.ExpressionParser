namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    // a special operator in that it only takes a single child node (that must be an operator node)
    public class NotNode : UnaryOperatorNode
    {
        public const string Literal = "!";

        public NotNode() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public NotNode(OperatorNode associatedNode) : base(AssociativityEnum.Right, PrecedenceEnum.Multiplicative, associatedNode)
        {
        }
    }
}
