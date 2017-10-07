using System.Collections.Generic;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Values;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests
{
    public class ExpressionTreeVisitorShould
    {
        [Fact]
        public void TraverseSimpleTree()
        {
            AdditionNode root = new AdditionNode(
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
            AdditionNode root = new AdditionNode(
                new MultiplicationNode(new IntValueNode(5), new IntValueNode(2)), 
                new IntValueNode(10));
            ExpressionTreeVisitor testSubject = new ExpressionTreeVisitor(root);
            List<ExpressionNode> result = new List<ExpressionNode>(testSubject);

            Assert.Equal(5, result.Count);
            Assert.True(result.Contains(root));
            MultiplicationNode multiplicationNode = (MultiplicationNode) root.Left;
            Assert.True(result.Contains(multiplicationNode));
            Assert.True(result.Contains(root.Right));
            Assert.True(result.Contains(multiplicationNode.Left));
            Assert.True(result.Contains(multiplicationNode.Right));
        }

        [Fact]
        public void TraverseTreeWithBinaryAndUnaryNodes()
        {
            AdditionNode root = new AdditionNode(
                new MultiplicationNode(new IntValueNode(5), new IntValueNode(2)),
                new NegateNode(new IntValueNode(1)));
            ExpressionTreeVisitor testSubject = new ExpressionTreeVisitor(root);
            List<ExpressionNode> result = new List<ExpressionNode>(testSubject);

            Assert.Equal(6, result.Count);
            Assert.True(result.Contains(root));
            MultiplicationNode multiplicationNode = (MultiplicationNode)root.Left;
            Assert.True(result.Contains(multiplicationNode));
            Assert.True(result.Contains(root.Right));
            Assert.True(result.Contains(multiplicationNode.Left));
            Assert.True(result.Contains(multiplicationNode.Right));
            NegateNode negateNode = (NegateNode) root.Right;
            Assert.True(result.Contains(negateNode.AssociatedNode));
        }
    }
}
