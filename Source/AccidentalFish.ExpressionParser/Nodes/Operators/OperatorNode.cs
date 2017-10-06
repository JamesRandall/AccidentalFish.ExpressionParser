namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public abstract class OperatorNode : ExpressionNode
    {
        public enum AssociativityEnum
        {
            Left,
            Right
        }

        public enum PrecedenceEnum
        {
            Conditional = 0,
            Equality=1,
            Relational=2,
            Additive=3,
            Multiplicative=4,
            Unary=5
        }

        protected OperatorNode(AssociativityEnum associativity, PrecedenceEnum precedence)
        {
            Associativity = associativity;
            Precedence = precedence;
        }

        public AssociativityEnum Associativity { get; }

        public PrecedenceEnum Precedence { get; }
    }
}
