using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class SubtractionOperator : BinaryOperatorNode
    {
        public const string Literal = "-";

        public SubtractionOperator() : base(AssociativityEnum.Left, PrecedenceEnum.Additive)
        {
        }

        public SubtractionOperator(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Additive, left, right)
        {
        }
    }
}
