using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class LessThanNode : BinaryOperatorNode
    {
        public const string Literal = "<";

        public LessThanNode() : base(AssociativityEnum.Left, PrecedenceEnum.Relational)
        {
        }

        public LessThanNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Equality, left, right)
        {
        }
    }
}
