using System;
using AccidentalFish.ExpressionParser.Parsers;

namespace AccidentalFish.ExpressionParser
{
    public static class Dependencies
    {
        public static void Register(Action<Type, Type> registerImplementationFunc)
        {
            registerImplementationFunc(typeof(IExpressionFactory), typeof(ExpressionFactory));
            registerImplementationFunc(typeof(IExpressionSplitter), typeof(ExpressionSplitter));
            registerImplementationFunc(typeof(IExpressionTreeBuilder), typeof(ExpressionTreeBuilder));
            registerImplementationFunc(typeof(IRpnExpressionBuilder), typeof(RpnExpressionBuilder));
            registerImplementationFunc(typeof(IParserProvider), typeof(ParserProvider));
        }
    }
}
