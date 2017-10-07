using System.Collections.Generic;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Parsers;

namespace AccidentalFish.ExpressionParser
{
    public sealed class ExpressionFactory : IExpressionFactory
    {
        private readonly IRpnExpressionBuilder _rpnExpressionBuilder;
        private readonly IExpressionSplitter _expressionSplitter;
        private readonly IExpressionTreeBuilder _expressionTreeBuilder;

        public ExpressionFactory(IRpnExpressionBuilder rpnExpressionBuilder,
            IExpressionSplitter expressionSplitter,
            IExpressionTreeBuilder expressionTreeBuilder)
        {
            _rpnExpressionBuilder = rpnExpressionBuilder;
            _expressionSplitter = expressionSplitter;
            _expressionTreeBuilder = expressionTreeBuilder;
        }

        ExpressionNode IExpressionFactory.Parse(string expression)
        {
            IReadOnlyCollection<ExpressionNode> components = _expressionSplitter.Split(expression);
            RpnExpression rpnExpression = _rpnExpressionBuilder.Build(components);
            ExpressionNode node = _expressionTreeBuilder.Build(rpnExpression);

            return node;
        }

        public static ExpressionNode Parse(string expression)
        {
            IExpressionFactory expressionFactory = new ExpressionFactory(
                new RpnExpressionBuilder(),
                new ExpressionSplitter(new ParserProvider()),
                new ExpressionTreeBuilder());
            return expressionFactory.Parse(expression);
        }

        public static ExpressionNode Parse(string expression, IParserProvider parserProvider)
        {
            IExpressionFactory expressionFactory = new ExpressionFactory(
                new RpnExpressionBuilder(),
                new ExpressionSplitter(parserProvider),
                new ExpressionTreeBuilder());
            return expressionFactory.Parse(expression);
        }
    }
}
