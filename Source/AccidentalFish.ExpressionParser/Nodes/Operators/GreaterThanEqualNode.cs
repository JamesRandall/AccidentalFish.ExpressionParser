using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class GreaterThanEqualNode : BinaryOperatorNode
    {
        public const string Literal = ">=";

        public GreaterThanEqualNode() : base(AssociativityEnum.Left, PrecedenceEnum.Relational)
        {
        }

        public GreaterThanEqualNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Equality, left, right)
        {
        }
    }
}
