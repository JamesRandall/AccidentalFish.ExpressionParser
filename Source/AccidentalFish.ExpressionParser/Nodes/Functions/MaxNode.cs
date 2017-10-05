using System;
using System.Collections.Generic;
using System.Text;

namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class MaxNode : VariableParamsFunctionNode
    {
        public const string Literal = "max";

        public MaxNode()
        {
        }

        public MaxNode(ExpressionNode[] parameters) : base(parameters)
        {
        }
    }
}
