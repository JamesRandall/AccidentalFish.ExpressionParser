using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class GreaterThanNode : BinaryOperatorNode
    {
        public const string Literal = ">";

        public GreaterThanNode() : base(AssociativityEnum.Left, PrecedenceEnum.Relational)
        {
        }

        public GreaterThanNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Equality, left, right)
        {
        }
    }
}
