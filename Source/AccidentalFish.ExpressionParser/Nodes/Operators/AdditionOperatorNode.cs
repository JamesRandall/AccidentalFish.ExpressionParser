using System.Linq.Expressions;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class AdditionOperatorNode : BinaryOperatorNode
    {
        public const string Literal = "+";

        public AdditionOperatorNode() : base(AssociativityEnum.Left, PrecedenceEnum.Additive)
        {
        }

        public AdditionOperatorNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Additive, left, right)
        {
        }
    }
}
