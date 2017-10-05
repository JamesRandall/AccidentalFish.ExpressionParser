using System.Collections;
using System.Collections.Generic;

namespace AccidentalFish.ExpressionParser.Nodes
{
    public abstract class ExpressionNode : IEnumerable<ExpressionNode>
    {
        public IEnumerator<ExpressionNode> GetEnumerator()
        {
            return new ExpressionTreeVisitor(this).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
