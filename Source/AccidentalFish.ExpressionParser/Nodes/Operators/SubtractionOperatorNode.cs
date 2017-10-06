using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class SubtractionOperatorNode : BinaryOperatorNode
    {
        public const string Literal = "-";

        public SubtractionOperatorNode() : base(AssociativityEnum.Left, PrecedenceEnum.Additive)
        {
        }

        public SubtractionOperatorNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Additive, left, right)
        {
        }
    }
}
