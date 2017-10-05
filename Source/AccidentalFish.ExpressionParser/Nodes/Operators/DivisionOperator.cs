using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class DivisionOperator : BinaryOperatorNode
    {
        public const string Literal = "/";

        public DivisionOperator() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public DivisionOperator(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
