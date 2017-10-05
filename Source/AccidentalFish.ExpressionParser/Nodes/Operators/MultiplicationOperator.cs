using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class MultiplicationOperator : BinaryOperatorNode
    {
        public const string Literal = "*";

        public MultiplicationOperator() : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative)
        {
        }

        public MultiplicationOperator(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Multiplicative, left, right)
        {
        }
    }
}
