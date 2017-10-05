using System.Collections.Generic;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser
{
    public interface IRpnExpressionBuilder
    {
        RpnExpression Build(IReadOnlyCollection<ExpressionNode> components);
    }
}