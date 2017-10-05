using System.Collections.Generic;

namespace AccidentalFish.ExpressionParser.Nodes.Functions
{
    public class VariableParamsFunctionNode : FunctionNode
    {
        public VariableParamsFunctionNode()
        {
            
        }

        public VariableParamsFunctionNode(ExpressionNode[] parameters)
        {
            Parameters = parameters;
        }

        public IReadOnlyCollection<ExpressionNode> Parameters { get; internal set; }
    }
}
