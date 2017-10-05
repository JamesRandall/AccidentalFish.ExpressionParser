using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Functions;
using AccidentalFish.ExpressionParser.Nodes.Operators;

namespace AccidentalFish.ExpressionParser
{
    public sealed class ExpressionTreeVisitor : IEnumerable<ExpressionNode>
    {
        private readonly ExpressionNode _root;

        public ExpressionTreeVisitor(ExpressionNode root)
        {
            _root = root;
        }

        public IEnumerator<ExpressionNode> GetEnumerator()
        {
            Stack<ExpressionNode> stack = new Stack<ExpressionNode>();
            stack.Push(_root);
            while (stack.Any())
            {
                ExpressionNode node = stack.Pop();
                yield return node;
                if (node is BinaryOperatorNode operatorNode)
                {
                    if (operatorNode.Left != null)
                    {
                        stack.Push(operatorNode.Left);
                    }
                    if (operatorNode.Right != null)
                    {
                        stack.Push(operatorNode.Right);
                    }
                }
                else if (node is VariableParamsFunctionNode functionNode)
                {
                    foreach (ExpressionNode parameterNode in functionNode.Parameters)
                    {
                        stack.Push(parameterNode);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
