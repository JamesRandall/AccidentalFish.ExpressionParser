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
                if (node is BinaryOperatorNode binaryOperatorNode)
                {
                    if (binaryOperatorNode.Left != null)
                    {
                        stack.Push(binaryOperatorNode.Left);
                    }
                    if (binaryOperatorNode.Right != null)
                    {
                        stack.Push(binaryOperatorNode.Right);
                    }
                }
                else if (node is UnaryOperatorNode unaryOperatorNode)
                {
                    if (unaryOperatorNode.AssociatedNode != null)
                    {
                        stack.Push(unaryOperatorNode.AssociatedNode);
                    }
                }
                else if (node is FunctionNode functionNode)
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
