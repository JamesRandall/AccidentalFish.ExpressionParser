using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class DivisionOperatorNode : BinaryOperatorNode
    {
        public const string Literal = "/";

        public DivisionOperatorNode() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public DivisionOperatorNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
