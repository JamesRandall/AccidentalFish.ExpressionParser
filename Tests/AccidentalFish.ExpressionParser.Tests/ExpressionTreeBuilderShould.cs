using System.Linq;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Functions;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Values;
using AccidentalFish.ExpressionParser.Parsers;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests
{
    public class ExpressionTreeBuilderShould
    {
        [Fact]
        public void ReturnSimpleTreeForAdditiveOperation()
        {
            ExpressionSplitter expressionSplitter = new ExpressionSplitter(new ParserProvider());
            RpnExpressionBuilder builder = new RpnExpressionBuilder();
            RpnExpression rpnExpression = builder.Build(expressionSplitter.Split("5+3"));

            ExpressionTreeBuilder testSubject = new ExpressionTreeBuilder();
            ExpressionNode result = testSubject.Build(rpnExpression);

            Assert.IsType<AdditionNode>(result);
            Assert.Equal(5,((IntValueNode)((AdditionNode)result).Left).Value);
            Assert.Equal(3, ((IntValueNode)((AdditionNode)result).Right).Value);
        }

        [Fact]
        public void ReturnSimpleTreeForAdditiveOperationWithVariable()
        {
            ExpressionSplitter expressionSplitter = new ExpressionSplitter(new ParserProvider());
            RpnExpressionBuilder builder = new RpnExpressionBuilder();
            RpnExpression rpnExpression = builder.Build(expressionSplitter.Split("5+@myvar"));

            ExpressionTreeBuilder testSubject = new ExpressionTreeBuilder();
            ExpressionNode result = testSubject.Build(rpnExpression);

            Assert.IsType<AdditionNode>(result);
            Assert.Equal(5, ((IntValueNode)((AdditionNode)result).Left).Value);
            Assert.Equal("@myvar", ((VariableNode)((AdditionNode)result).Right).Name);
        }

        [Fact]
        public void ReturnsBracketedMultiplicationAndAddition()
        {
            ExpressionSplitter expressionSplitter = new ExpressionSplitter(new ParserProvider());
            RpnExpressionBuilder builder = new RpnExpressionBuilder();
            RpnExpression rpnExpression = builder.Build(expressionSplitter.Split("(5+3)*2"));

            ExpressionTreeBuilder testSubject = new ExpressionTreeBuilder();
            ExpressionNode result = testSubject.Build(rpnExpression);

            Assert.IsType<MultiplicationNode>(result);
            Assert.IsType<AdditionNode>(((MultiplicationNode)result).Left);
        }

        [Fact]
        public void BuildsSimpleTreeWithFunctionRoot()
        {
            ExpressionSplitter expressionSplitter = new ExpressionSplitter(new ParserProvider());
            RpnExpressionBuilder builder = new RpnExpressionBuilder();
            RpnExpression rpnExpression = builder.Build(expressionSplitter.Split("max(5,3)"));

            ExpressionTreeBuilder testSubject = new ExpressionTreeBuilder();
            ExpressionNode result = testSubject.Build(rpnExpression);

            Assert.IsType<MaxNode>(result);
            IntValueNode firstParameter = ((MaxNode) result).Parameters.First() as IntValueNode;
            IntValueNode secondParameter = ((MaxNode)result).Parameters.Last() as IntValueNode;
            Assert.NotNull(firstParameter);
            Assert.Equal(5, firstParameter.Value);
            Assert.NotNull(secondParameter);
            Assert.Equal(3, secondParameter.Value);
            Assert.Equal(2, ((MaxNode)result).Parameters.Count);
        }

        [Fact]
        public void BuildsTreeWithDeepFunctionRoot()
        {
            ExpressionSplitter expressionSplitter = new ExpressionSplitter(new ParserProvider());
            RpnExpressionBuilder builder = new RpnExpressionBuilder();
            RpnExpression rpnExpression = builder.Build(expressionSplitter.Split("4*max(5,3)"));

            ExpressionTreeBuilder testSubject = new ExpressionTreeBuilder();
            ExpressionNode result = testSubject.Build(rpnExpression);

            MultiplicationNode multiplicationNode = result as MultiplicationNode;
            Assert.NotNull(multiplicationNode);
            IntValueNode multiplierIntValueNode = multiplicationNode.Left as IntValueNode;
            Assert.NotNull(multiplierIntValueNode);
            Assert.Equal(4, multiplierIntValueNode.Value);
            MaxNode maxNode = multiplicationNode.Right as MaxNode;
            Assert.NotNull(maxNode);
            IntValueNode firstParameter = maxNode.Parameters.First() as IntValueNode;
            IntValueNode secondParameter = maxNode.Parameters.Last() as IntValueNode;
            Assert.NotNull(firstParameter);
            Assert.Equal(5, firstParameter.Value);
            Assert.NotNull(secondParameter);
            Assert.Equal(3, secondParameter.Value);
            Assert.Equal(2, maxNode.Parameters.Count);
        }
    }
}

