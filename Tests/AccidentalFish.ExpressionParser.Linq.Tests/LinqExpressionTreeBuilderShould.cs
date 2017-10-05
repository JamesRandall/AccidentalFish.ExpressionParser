using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AccidentalFish.ExpressionParser.Nodes;
using Xunit;

namespace AccidentalFish.ExpressionParser.Linq.Tests
{
    public class LinqExpressionTreeBuilderShould
    {
        [Fact]
        public void CreateIntegerAdditionExpression()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("5+3");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Expression expression = testSubject.Build(node);

            var lambda = Expression.Lambda<Func<int>>(expression);
            var func = lambda.Compile();
            var result = func();

            Assert.Equal(8, result);
        }

        [Fact]
        public void CreateSimpleTwoOperatorExpression()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("5+3*2");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Expression expression = testSubject.Build(node);

            var lambda = Expression.Lambda<Func<int>>(expression);
            var func = lambda.Compile();
            var result = func();

            Assert.Equal(11, result);
        }

        [Fact]
        public void CreateSimpleExpressionIncludingNegation()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("5+-3");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Expression expression = testSubject.Build(node);

            var lambda = Expression.Lambda<Func<int>>(expression);
            var func = lambda.Compile();
            var result = func();

            Assert.Equal(2, result);
        }

        [Fact]
        public void CreateSimpleExpressionIncludingDoubleNegation()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("5+--3");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Expression expression = testSubject.Build(node);

            var lambda = Expression.Lambda<Func<int>>(expression);
            var func = lambda.Compile();
            var result = func();

            Assert.Equal(8, result);
        }

        [Fact]
        public void CreateSimpleExpressionIncludingUnrequiredPositiveSigns()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("5-+++3");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Expression expression = testSubject.Build(node);

            var lambda = Expression.Lambda<Func<int>>(expression);
            var func = lambda.Compile();
            var result = func();

            Assert.Equal(2, result);
        }

        [Fact]
        public void CreateBracketedOperatorExpression()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("(5+3)*2");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Expression expression = testSubject.Build(node);

            var lambda = Expression.Lambda<Func<int>>(expression);
            var func = lambda.Compile();
            var result = func();

            Assert.Equal(16, result);
        }

        [Fact]
        public void CreateAdditionExpressionWithVariable()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("5+@myvar");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Dictionary<string, ParameterExpression> parameters = new Dictionary<string, ParameterExpression>
            {
                {"@myvar", Expression.Parameter(typeof(int), "@myvar")}
            };
            Expression expression = testSubject.Build(node, parameters);

            var lambda = Expression.Lambda<Func<int, int>>(expression, parameters.Values);
            var func = lambda.Compile();
            var result = func(3);

            Assert.Equal(8, result);
        }

        [Fact]
        public void CreateAdditionExpressionWithVariableAndNegation()
        {
            ExpressionNode node = ExpressionFactory.ParseExpression("5+-@myvar");
            LinqExpressionTreeBuilder testSubject = new LinqExpressionTreeBuilder();
            Dictionary<string, ParameterExpression> parameters = new Dictionary<string, ParameterExpression>
            {
                {"@myvar", Expression.Parameter(typeof(int), "@myvar")}
            };
            Expression expression = testSubject.Build(node, parameters);

            var lambda = Expression.Lambda<Func<int, int>>(expression, parameters.Values);
            var func = lambda.Compile();
            var result = func(3);

            Assert.Equal(2, result);
        }
    }
}
