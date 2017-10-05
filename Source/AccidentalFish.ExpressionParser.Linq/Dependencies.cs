using System;

namespace AccidentalFish.ExpressionParser.Linq
{
    public static class Dependencies
    {
        public static void Register(Action<Type, Type> registerImplementationFunc)
        {
            registerImplementationFunc(typeof(ILinqExpressionTreeBuilder), typeof(LinqExpressionTreeBuilder));
        }
    }
}
