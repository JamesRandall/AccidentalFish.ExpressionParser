using System;
using AccidentalFish.ExpressionParser.Nodes;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public interface IParser
    {
        bool IsPartialMatch(string partialToken, ExpressionNode last);

        bool IsCompleteMatch(string token);

        Func<string, ExpressionNode> Factory { get; }
    }
}
