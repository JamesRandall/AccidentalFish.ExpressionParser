using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AccidentalFish.ExpressionParser.Linq.Exceptions;
using AccidentalFish.ExpressionParser.Nodes.Operators;

namespace AccidentalFish.ExpressionParser.Linq.Extensions
{
    internal static class OperatorNodeExtensions
    {
        private static readonly Dictionary<Type, Func<Expression, Expression, Expression>> BinaryFactories =
            new Dictionary<Type, Func<Expression, Expression, Expression>>
            {
                {typeof(AdditionNode), Expression.Add},
                {typeof(ConditionalAndNode), Expression.AndAlso},
                {typeof(ConditionalOrNode), Expression.OrElse},
                {typeof(DivisionNode), Expression.Divide},
                {typeof(EqualNode), Expression.Equal},
                {typeof(GreaterThanEqualNode), Expression.GreaterThanOrEqual},
                {typeof(GreaterThanNode), Expression.GreaterThan},
                {typeof(LessThanEqualNode), Expression.LessThan},
                {typeof(LessThanNode), Expression.LessThan},
                {typeof(MultiplicationNode), Expression.Multiply},
                {typeof(NotEqualNode), Expression.NotEqual},
                {typeof(SubtractionNode), Expression.Subtract},
                {typeof(ModuloNode), Expression.Modulo }
            };

        private static readonly Dictionary<Type, Func<Expression, Expression>> UnaryFactories = 
            new Dictionary<Type, Func<Expression, Expression>>
            {
                {typeof(NotNode), Expression.Not },
                {typeof(NegateNode),Expression.Negate}
            };

        public static Expression CreateLinq(this BinaryOperatorNode operatorNode, Expression left, Expression right)
        {
            if (BinaryFactories.TryGetValue(operatorNode.GetType(), out Func<Expression, Expression, Expression> factory))
            {
                return factory(left, right);
            }
            throw new UnsupportedExpressionNodeTypeException("Expression node type not supported in LINQ expression building", operatorNode.GetType());
        }

        public static Expression CreateLinq(this UnaryOperatorNode operatorNode, Expression node)
        {
            if (UnaryFactories.TryGetValue(operatorNode.GetType(), out Func<Expression, Expression> factory))
            {
                return factory(node);
            }
            throw new UnsupportedExpressionNodeTypeException("Expression node type not supported in LINQ expression building", operatorNode.GetType());
        }
    }
}
