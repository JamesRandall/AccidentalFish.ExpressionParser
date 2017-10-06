using System.Collections.Generic;
using System.Linq;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Values;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests.Unit
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
                new VariableNode("@myVariablke")
            };
            RpnExpressionBuilder testSubject = new RpnExpressionBuilder();

            ExpressionNode[] rpnExpression = testSubject.Build(components).ToArray();

            Assert.IsType<IntValueNode>(rpnExpression[0]);
            Assert.IsType<VariableNode>(rpnExpression[1]);
            Assert.IsType<AdditionNode>(rpnExpression[2]);
        }
    }
}
