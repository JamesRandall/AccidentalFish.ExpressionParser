using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class LessThanEqualNode : BinaryOperatorNode
    {
        public const string Literal = "<=";

        public LessThanEqualNode() : base(AssociativityEnum.Left, PrecedenceEnum.Relational)
        {
        }

        public LessThanEqualNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Equality, left, right)
        {
        }
    }
}
