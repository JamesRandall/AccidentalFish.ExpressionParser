using System.Collections.Generic;
using System.Linq.Expressions;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Linq
{
    public interface ILinqExpressionTreeBuilder
    {
        Expression Build(ExpressionNode node, Dictionary<string, ParameterExpression> parameters=null);
    }
}