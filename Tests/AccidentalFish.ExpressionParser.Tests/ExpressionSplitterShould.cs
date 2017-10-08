using System.Collections.Generic;
using System.Linq;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Structural;
using AccidentalFish.ExpressionParser.Nodes.Values;
using AccidentalFish.ExpressionParser.Parsers;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests
{
    public class ExpressionSplitterShould
    {
        [Fact]
        public void ReturnSingleDoubleExpressionNode()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            IReadOnlyCollection<ExpressionNode> result = testSubject.Split("5.5");

            Assert.Equal(1, result.Count);
            Assert.Equal(5.5, ((DoubleValueNode)result.Single()).Value);
        }

        [Fact]
        public void ReturnSingleIntExpressionNode()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            IReadOnlyCollection<ExpressionNode> result = testSubject.Split("5");

            Assert.Equal(1, result.Count);
            Assert.Equal(5, ((IntValueNode)result.Single()).Value);
        }

        [Fact]
        public void ReturnNodesForSimpleMultipartExpression()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5+3").ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<AdditionNode>(result[1]);
            Assert.Equal(3, ((IntValueNode) result[2]).Value);
        }

        [Fact]
        public void IgnoreWhitespace()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5 + 3 ").ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<AdditionNode>(result[1]);
            Assert.Equal(3, ((IntValueNode)result[2]).Value);
        }

        [Fact]
        public void ReturnNodesForSimpleMultipartExpressionWithEqualityOperator()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5+3==8").ToArray();

            Assert.Equal(5, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<AdditionNode>(result[1]);
            Assert.Equal(3, ((IntValueNode)result[2]).Value);
            Assert.IsType<EqualNode>(result[3]);
            Assert.Equal(8, ((IntValueNode)result[4]).Value);
        }

        [Fact]
        public void ReturnsNodeIncludingNegativeUnaryOperator()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5+-3").ToArray();
            Assert.Equal(4, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<AdditionNode>(result[1]);
            Assert.IsType<NegateNode>(result[2]);
            Assert.Equal(3, ((IntValueNode)result[3]).Value);
        }

        [Fact]
        public void IgnoresPositiveLiteralsThatHaveNoEffect()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5+-+3").ToArray();
            Assert.Equal(4, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<AdditionNode>(result[1]);
            Assert.IsType<NegateNode>(result[2]);
            Assert.Equal(3, ((IntValueNode)result[3]).Value);
        }

        [Fact]
        public void IgnoresMultiplePositiveLiteralsThatHaveNoEffect()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5++-++3").ToArray();
            Assert.Equal(4, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<AdditionNode>(result[1]);
            Assert.IsType<NegateNode>(result[2]);
            Assert.Equal(3, ((IntValueNode)result[3]).Value);
        }

        [Fact]
        public void ReturnNodesForMultipartExpressionWithVariables()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5+@first==@second").ToArray();

            Assert.Equal(5, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<AdditionNode>(result[1]);
            Assert.IsType<VariableNode>(result[2]);
            Assert.IsType<EqualNode>(result[3]);
            Assert.IsType<VariableNode>(result[4]);
            Assert.Equal("@second", ((VariableNode)result[4]).Name);
        }

        [Fact]
        public void SplitBracketedExpression()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("(9+3)/2").ToArray();

            Assert.Equal(7, result.Length);
            Assert.IsType<OpenBracketNode>(result[0]);
            Assert.Equal(9, ((IntValueNode)result[1]).Value);
            Assert.IsType<AdditionNode>(result[2]);
            Assert.Equal(3, ((IntValueNode)result[3]).Value);
            Assert.IsType<CloseBracketNode>(result[4]);
            Assert.IsType<DivisionNode>(result[5]);
        }

        [Fact]
        public void CorrectlyDifferentiateLessThanOperator()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5 < 10000").ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal(5, ((IntValueNode) result[0]).Value);
            Assert.IsType<LessThanNode>(result[1]);
            Assert.Equal(10000, ((IntValueNode) result[2]).Value);
        }

        [Fact]
        public void CorrectlyDifferentiateLessThanEqualOperator()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("5 <= 10000").ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal(5, ((IntValueNode)result[0]).Value);
            Assert.IsType<LessThanEqualNode>(result[1]);
            Assert.Equal(10000, ((IntValueNode)result[2]).Value);
        }

        [Fact]
        public void SplitsSimpleFunction()
        {
            ExpressionSplitter testSubject = new ExpressionSplitter(new ParserProvider());
            ExpressionNode[] result = testSubject.Split("max(2,3)").ToArray();
        }
    }
}
