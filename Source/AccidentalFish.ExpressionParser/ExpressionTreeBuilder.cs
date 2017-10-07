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
                else if (node is FunctionNode functionNode)
                {
                    ExpressionNode[] parameters = new ExpressionNode[functionNode.NumberOfParameters()];
                    for (int index = 0; index < functionNode.NumberOfParameters(); index++)
                    {
                        parameters[functionNode.NumberOfParameters() - index - 1] = shuntingStack.Pop();
                    }
                    functionNode.Parameters = parameters;
                }
                shuntingStack.Push(node);
            }

            return shuntingStack.Pop();
        }
    }
}
