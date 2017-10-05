using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class NotEqualNode : BinaryOperatorNode
    {
        public const string Literal = "!=";

        public NotEqualNode() : base(AssociativityEnum.Left, PrecedenceEnum.Equality)
        {
        }

        public NotEqualNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Equality, left, right)
        {
        }
    }
}
