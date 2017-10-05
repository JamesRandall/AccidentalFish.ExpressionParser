namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class UnaryOperatorNode : OperatorNode
    {
        protected UnaryOperatorNode(AssociativityEnum associativity, PrecedenceEnum precedence) : base(associativity, precedence)
        {
        }

        protected UnaryOperatorNode(AssociativityEnum associativity, PrecedenceEnum precedence, ExpressionNode associatedNode) : this(associativity, precedence)
        {
            AssociatedNode = associatedNode;
        }

        public ExpressionNode AssociatedNode { get; internal set; }
    }
}
