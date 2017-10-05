using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser
{
    public interface IExpressionFactory
    {
        ExpressionNode Parse(string expression);
    }
}
