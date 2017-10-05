using System.Collections.Generic;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Functions;
using AccidentalFish.ExpressionParser.Nodes.Operators;

namespace AccidentalFish.ExpressionParser
{
    public sealed class ExpressionTreeBuilder : IExpressionTreeBuilder
    {
        public ExpressionNode Build(RpnExpression rpnExpression)
        {
            Stack<ExpressionNode> shuntingStack = new Stack<ExpressionNode>();

            foreach (ExpressionNode node in rpnExpression)
            {
                if (node is BinaryOperatorNode operatorNode)
                {
                    operatorNode.Right = shuntingStack.Pop();
                    operatorNode.Left = shuntingStack.Pop();
                }
                else if (node is UnaryOperatorNode unaryOperatorNode)
                {
                    unaryOperatorNode.AssociatedNode = shuntingStack.Pop();
                }
                else if (node is VariableParamsFunctionNode functionNode)
                {
                    // TODO: nneds work
                    for (int index = 0; index < functionNode.Parameters.Count; index++)
                    {
                        ExpressionNode parameterNode = shuntingStack.Pop();
                    }
                }
                shuntingStack.Push(node);
            }

            return shuntingStack.Pop();
        }
    }
}
