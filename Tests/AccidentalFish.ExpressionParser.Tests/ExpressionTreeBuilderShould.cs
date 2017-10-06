using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Values;
using AccidentalFish.ExpressionParser.Parsers;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests.Unit
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

            Assert.IsType<AdditionOperatorNode>(result);
            Assert.Equal(5,((IntValueNode)((AdditionOperatorNode)result).Left).Value);
            Assert.Equal(3, ((IntValueNode)((AdditionOperatorNode)result).Right).Value);
        }

        [Fact]
        public void ReturnSimpleTreeForAdditiveOperationWithVariable()
        {
            ExpressionSplitter expressionSplitter = new ExpressionSplitter(new ParserProvider());
            RpnExpressionBuilder builder = new RpnExpressionBuilder();
            RpnExpression rpnExpression = builder.Build(expressionSplitter.Split("5+@myvar"));

            ExpressionTreeBuilder testSubject = new ExpressionTreeBuilder();
            ExpressionNode result = testSubject.Build(rpnExpression);

            Assert.IsType<AdditionOperatorNode>(result);
            Assert.Equal(5, ((IntValueNode)((AdditionOperatorNode)result).Left).Value);
            Assert.Equal("@myvar", ((VariableNode)((AdditionOperatorNode)result).Right).Name);
        }

        [Fact]
        public void ReturnsBracketedMultiplicationAndAddition()
        {
            ExpressionSplitter expressionSplitter = new ExpressionSplitter(new ParserProvider());
            RpnExpressionBuilder builder = new RpnExpressionBuilder();
            RpnExpression rpnExpression = builder.Build(expressionSplitter.Split("(5+3)*2"));

            ExpressionTreeBuilder testSubject = new ExpressionTreeBuilder();
            ExpressionNode result = testSubject.Build(rpnExpression);

            Assert.IsType<MultiplicationOperatorNode>(result);
            Assert.IsType<AdditionOperatorNode>(((MultiplicationOperatorNode)result).Left);
        }
    }
}
