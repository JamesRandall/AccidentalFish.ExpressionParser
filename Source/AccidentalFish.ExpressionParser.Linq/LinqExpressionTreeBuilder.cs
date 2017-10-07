using System.Collections.Generic;
using System.Linq.Expressions;
using AccidentalFish.ExpressionParser.Linq.Exceptions;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Functions;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Values;
using AccidentalFish.ExpressionParser.Linq.Extensions;

namespace AccidentalFish.ExpressionParser.Linq
{
    public class LinqExpressionTreeBuilder : ILinqExpressionTreeBuilder
    {
        // TODO: Intention of this is to (attempt!) to build a linq expression tree
        // that can then be compiled into a lambda
        Expression ILinqExpressionTreeBuilder.Build(ExpressionNode node, Dictionary<string, ParameterExpression> parameters)
        {
            Expression result = RecursivelyBuild(node, parameters);

            return result;
        }

        public static Expression Build(ExpressionNode node, Dictionary<string, ParameterExpression> parameters = null)
        {
            return ((ILinqExpressionTreeBuilder)new LinqExpressionTreeBuilder()).Build(node, parameters);
        }

        private Expression RecursivelyBuild(ExpressionNode node, Dictionary<string, ParameterExpression> parameters)
        {
            if (node is BinaryOperatorNode operatorNode)
            {
                Expression left = RecursivelyBuild(operatorNode.Left, parameters);
                Expression right = RecursivelyBuild(operatorNode.Right, parameters);

                return operatorNode.CreateLinq(left, right);
            }
            if (node is UnaryOperatorNode unaryOperatorNode)
            {
                Expression associatedNode = RecursivelyBuild(unaryOperatorNode.AssociatedNode, parameters);
                return unaryOperatorNode.CreateLinq(associatedNode);
            }
            if (node is ValueNode valueNode)
            {
                return valueNode.CreateLinq(parameters);
            }
            if (node is FunctionNode)
            {
                throw new UnsupportedExpressionNodeTypeException("Functions are wip", node.GetType());
            }

            throw new UnsupportedExpressionNodeTypeException($"Unsupported node type", node.GetType());
        }
    }
}
