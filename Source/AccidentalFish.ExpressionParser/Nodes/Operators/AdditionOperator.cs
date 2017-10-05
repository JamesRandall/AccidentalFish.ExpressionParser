using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class AdditionOperator : BinaryOperatorNode
    {
        public const string Literal = "+";

        public AdditionOperator() : base(AssociativityEnum.Left, PrecedenceEnum.Additive)
        {
        }

        public AdditionOperator(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Additive, left, right)
        {
        }
    }
}
