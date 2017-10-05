using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser
{
    public interface IExpressionTreeBuilder
    {
        ExpressionNode Build(RpnExpression rpnExpression);
    }
}