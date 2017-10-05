using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AccidentalFish.ExpressionParser.Nodes.Operators
{
    public class ConditionalAndNode : BinaryOperatorNode
    {
        public const string Literal = "&&";

        public ConditionalAndNode() : base(AssociativityEnum.Left, PrecedenceEnum.Relational)
        {
        }

        public ConditionalAndNode(ExpressionNode left, ExpressionNode right) : base(AssociativityEnum.Left, PrecedenceEnum.Relational, left, right)
        {
        }
    }
}
