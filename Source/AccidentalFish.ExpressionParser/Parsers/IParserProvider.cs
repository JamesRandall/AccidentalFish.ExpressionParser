using System.Collections.Generic;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public interface IParserProvider
    {
        IReadOnlyCollection<IParser> Get();
    }
}