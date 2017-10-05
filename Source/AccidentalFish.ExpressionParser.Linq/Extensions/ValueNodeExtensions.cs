using System.Collections.Generic;
using System.Linq.Expressions;
using AccidentalFish.ExpressionParser.Linq.Exceptions;
using AccidentalFish.ExpressionParser.Nodes.Values;

namespace AccidentalFish.ExpressionParser.Linq.Extensions
{
    internal static class ValueNodeExtensions
    {
        public static Expression CreateLinq(this ValueNode valueNode, Dictionary<string, ParameterExpression> parameters)
        {
            if (valueNode is IntValueNode intValueNode)
            {
                return Expression.Constant(intValueNode.Value);
            }
            if (valueNode is DoubleValueNode doubleValueNode)
            {
                return Expression.Constant(doubleValueNode.Value);
            }
            if (valueNode is VariableNode variableNode)
            {
                if (parameters == null || !parameters.TryGetValue(variableNode.Name, out var parameter))
                {
                    throw new LinqExpressionCompilationException($"Missing parameter for variable {variableNode.Name}");
                }
                return parameter;
            }
            throw new UnsupportedExpressionNodeTypeException("Expression node type not supported in LINQ expression building", valueNode.GetType());
        }
    }
}
