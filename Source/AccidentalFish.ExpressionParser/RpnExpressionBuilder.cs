using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Functions;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Structural;
using AccidentalFish.ExpressionParser.Nodes.Values;

namespace AccidentalFish.ExpressionParser
{
    public sealed class RpnExpressionBuilder : IRpnExpressionBuilder
    {
        public RpnExpression Build(IReadOnlyCollection<ExpressionNode> components)
        {
            Stack<ExpressionNode> shuntingStack = new Stack<ExpressionNode>();
            List<ExpressionNode> result = new List<ExpressionNode>();

            foreach (ExpressionNode component in components)
            {
                if (component is FunctionNode)
                {
                    shuntingStack.Push(component);
                    // if we want to add support for functions that support variable lengths
                    // of parameters we will need to drop a parameter start marker onto result
                    // here. E.g.:
                    //    result.Add(new ParameterSetBeginMarkerNode())
                }
                else if (component is ParameterDelimiterNode)
                {
                    while (!(shuntingStack.Peek() is OpenBracketNode))
                    {
                        result.Add(shuntingStack.Pop());
                    }
                }
                else if (component is OperatorNode operatorNode)
                {
                    HandleOperator(shuntingStack, operatorNode, result);
                }
                else if (component is ValueNode)
                {
                    result.Add(component);
                }
                else if (component is OpenBracketNode)
                {
                    shuntingStack.Push(component);
                }
                else if (component is CloseBracketNode)
                {
                    while (!(shuntingStack.Peek() is OpenBracketNode))
                    {
                        result.Add(shuntingStack.Pop());
                    }
                    shuntingStack.Pop(); // remove the open bracket node
                    if (shuntingStack.Any() && shuntingStack.Peek() is FunctionNode)
                    {
                        result.Add(shuntingStack.Pop());
                    }
                }
                else
                {
                    Debug.Assert(false, $"Unexpected expression node type {component.GetType().Name}");
                }
            }

            while (shuntingStack.Any())
            {
                result.Add(shuntingStack.Pop());
            }

            return new RpnExpression(result);
        }

        private static void HandleOperator(Stack<ExpressionNode> shuntingStack, OperatorNode operatorNode, List<ExpressionNode> result)
        {
            if (shuntingStack.Any())
            {
                if (shuntingStack.Peek() is OperatorNode head)
                {
                    if ((operatorNode.Associativity == OperatorNode.AssociativityEnum.Left &&
                         operatorNode.Precedence <= head.Precedence) ||
                        (operatorNode.Associativity == OperatorNode.AssociativityEnum.Right &&
                         operatorNode.Precedence < head.Precedence))
                    {
                        shuntingStack.Pop();
                        result.Add(head);
                    }
                }
            }
            shuntingStack.Push(operatorNode);
        }
    }
}
