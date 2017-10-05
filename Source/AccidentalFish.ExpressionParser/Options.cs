using System.Collections.Generic;

namespace AccidentalFish.ExpressionParser
{
    public sealed class Options
    {
        public ISet<string> WhitespaceCharacters { get; set; }

        public static readonly Options Default = new Options
        {
            WhitespaceCharacters = new HashSet<string> {" ", "\t"}
        };
    }
}
