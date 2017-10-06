using System.Collections.Generic;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Values;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests.Unit
{
    public class ExpressionTreeVisitorShould
    {
        [Fact]
        public void TraverseSimpleTree()
        {
            AdditionOperatorNode root = new AdditionOperatorNode(
                new IntValueNode(5),
                new IntValueNode(10));
            ExpressionTreeVisitor testSubject = new ExpressionTreeVisitor(root);
            List<ExpressionNode> result = new List<ExpressionNode>(testSubject);

            Assert.Equal(3, result.Count);
            Assert.True(result.Contains(root));
            Assert.True(result.Contains(root.Left));
            Assert.True(result.Contains(root.Right));
        }

        [Fact]
        public void TraverseShallowTree()
        {
            AdditionOperatorNode root = new AdditionOperatorNode(
                new MultiplicationOperatorNode(new IntValueNode(5), new IntValueNode(2)), 
                new IntValueNode(10));
            ExpressionTreeVisitor testSubject = new ExpressionTreeVisitor(root);
            List<ExpressionNode> result = new List<ExpressionNode>(testSubject);

            Assert.Equal(5, result.Count);
            Assert.True(result.Contains(root));
            MultiplicationOperatorNode multiplicationOperatorNode = (MultiplicationOperatorNode) root.Left;
            Assert.True(result.Contains(multiplicationOperatorNode));
            Assert.True(result.Contains(root.Right));
            Assert.True(result.Contains(multiplicationOperatorNode.Left));
            Assert.True(result.Contains(multiplicationOperatorNode.Right));
        }
    }
}
