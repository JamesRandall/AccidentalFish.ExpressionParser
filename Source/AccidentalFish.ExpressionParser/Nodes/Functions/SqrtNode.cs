using System;
using System.Collections.Generic;
using System.Text;

namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class SqrtNode : FunctionNode
    {
        public const string Literal = "sqrt";

        public SqrtNode()
        {
        }

        public SqrtNode(ExpressionNode[] parameters) : base(parameters)
        {
        }

        public override int NumberOfParameters()
        {
            return 1;
        }
    }
}
