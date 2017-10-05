using System.Collections;
using System.Collections.Generic;

namespace AccidentalFish.ExpressionParser.Nodes
{
    public class RpnExpression : IEnumerable<ExpressionNode>
    {
        private readonly IReadOnlyCollection<ExpressionNode> _nodes;

        internal RpnExpression(IReadOnlyCollection<ExpressionNode> nodes)
        {
            _nodes = nodes;
        }

        public IEnumerator<ExpressionNode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IReadOnlyCollection<ExpressionNode> Nodes => _nodes;
    }
}
