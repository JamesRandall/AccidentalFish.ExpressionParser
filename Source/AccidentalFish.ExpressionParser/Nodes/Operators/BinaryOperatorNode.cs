namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class BinaryOperatorNode : OperatorNode
    {
        protected BinaryOperatorNode(AssociativityEnum associativity, PrecedenceEnum precedence) : base(associativity, precedence)
        {
        }

        protected BinaryOperatorNode(AssociativityEnum associativity, PrecedenceEnum precedence, ExpressionNode left, ExpressionNode right) : this(associativity, precedence)
        {
            Left = left;
            Right = right;
        }

        public ExpressionNode Left { get; internal set; }

        public ExpressionNode Right { get; internal set; }
    }
}
