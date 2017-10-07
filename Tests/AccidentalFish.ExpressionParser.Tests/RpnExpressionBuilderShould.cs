using System.Collections.Generic;
using System.Linq;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Functions;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Structural;
using AccidentalFish.ExpressionParser.Nodes.Values;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests
{
    public class RpnExpressionBuilderShould
    {
        [Fact]
        public void ReturnSimpleExpressionAsRpn()
        {
            List<ExpressionNode> components = new List<ExpressionNode>
            {
                new IntValueNode(5),
                new AdditionNode(),
                new IntValueNode(10)
            };
            RpnExpressionBuilder testSubject = new RpnExpressionBuilder();

            ExpressionNode[] rpnExpression = testSubject.Build(components).ToArray();

            Assert.IsType<IntValueNode>(rpnExpression[0]);
            Assert.IsType<IntValueNode>(rpnExpression[1]);
            Assert.IsType<AdditionNode>(rpnExpression[2]);
        }

        [Fact]
        public void ReturnExpressionIncludingVariable()
        {
            List<ExpressionNode> components = new List<ExpressionNode>
            {
                new IntValueNode(5),
                new AdditionNode(),
                new VariableNode("@myVariable")
            };
            RpnExpressionBuilder testSubject = new RpnExpressionBuilder();

            ExpressionNode[] rpnExpression = testSubject.Build(components).ToArray();

            Assert.IsType<IntValueNode>(rpnExpression[0]);
            Assert.IsType<VariableNode>(rpnExpression[1]);
            Assert.IsType<AdditionNode>(rpnExpression[2]);
        }

        [Fact]
        public void ReturnExpressionForFunction()
        {
            List<ExpressionNode> components = new List<ExpressionNode>
            {
                new MaxNode(),
                new OpenBracketNode(),
                new IntValueNode(3),
                new ParameterDelimiterNode(),
                new IntValueNode(5),
                new CloseBracketNode()
            };
            RpnExpressionBuilder testSubject = new RpnExpressionBuilder();

            ExpressionNode[] rpnExpression = testSubject.Build(components).ToArray();

            Assert.IsType<IntValueNode>(rpnExpression[0]);
            Assert.IsType<IntValueNode>(rpnExpression[1]);
            Assert.IsType<MaxNode>(rpnExpression[2]);
        }
    }
}
