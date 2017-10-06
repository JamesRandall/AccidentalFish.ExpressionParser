using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class MultiplicationOperatorNode : BinaryOperatorNode
    {
        public const string Literal = "*";

        public MultiplicationOperatorNode() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public MultiplicationOperatorNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
