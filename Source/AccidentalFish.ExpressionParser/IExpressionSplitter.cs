using System.Collections.Generic;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser
{
    public interface IExpressionSplitter
    {
        IReadOnlyCollection<ExpressionNode> Split(string expression);
    }
}