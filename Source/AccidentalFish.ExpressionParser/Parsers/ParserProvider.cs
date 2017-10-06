using System.Collections.Generic;
using System.Linq;
using AccidentalFish.ExpressionParser.Nodes;
using AccidentalFish.ExpressionParser.Nodes.Functions;
using AccidentalFish.ExpressionParser.Nodes.Operators;
using AccidentalFish.ExpressionParser.Nodes.Structural;
using AccidentalFish.ExpressionParser.Nodes.Values;

namespace AccidentalFish.ExpressionParser.Parsers
{
    public class ParserProvider : IParserProvider
    {
        private static readonly IReadOnlyCollection<IParser> DefaultParsers;

        static ParserProvider()
        {
            DefaultParsers = new List<IParser>
            {
                // TODO: I am wandering in the below if we an always say that a node cannot be preceded by a node of the same type.
                // I think even with functions and function parameters that is still the case.

                // Functions
                new SimpleLiteralParser(MaxNode.Literal, token => new MaxNode()),
                new SimpleLiteralParser(MinNode.Literal, token => new MinNode()),
                new SimpleLiteralParser(ParameterSeparatorNode.Literal, token => new ParameterSeparatorNode()),
                // Operators
                new SimpleLiteralParser(ConditionalAndNode.Literal, token => new ConditionalAndNode()),
                new SimpleLiteralParser(EqualNode.Literal, token => new EqualNode()),
                new SimpleLiteralParser(GreaterThanEqualNode.Literal, token => new GreaterThanEqualNode()),
                new SimpleLiteralParser(GreaterThanNode.Literal, token => new GreaterThanNode()),
                new SimpleLiteralParser(LessThanEqualNode.Literal, token => new LessThanEqualNode()),
                new SimpleLiteralParser(LessThanNode.Literal, token => new LessThanNode()),
                new SimpleLiteralParser(NotEqualNode.Literal, token => new NotEqualNode()),
                new SimpleLiteralParser(NotNode.Literal, token => new NotNode()),
                new SimpleLiteralParser(ConditionalOrNode.Literal, token => new ConditionalOrNode()),
                new LookbackLiteralParser(AdditionNode.Literal, // we use a lookback operator to deal with negation and positive syntax e.g. 5+-3
                    (previous) => !(previous is OperatorNode),
                    token => new AdditionNode()),
                new LookbackLiteralParser(SubtractionNode.Literal,
                    (previous) => !(previous is OperatorNode),
                    token => new SubtractionNode()),
                new SimpleLiteralParser(MultiplicationNode.Literal, token => new MultiplicationNode()),
                new SimpleLiteralParser(PowerOperatorNode.Literal, token => new PowerOperatorNode()),
                new SimpleLiteralParser(DivisionNode.Literal, token => new DivisionNode()),
                new LookbackLiteralParser(
                    NegateNode.Literal,
                    previous => previous is OperatorNode,
                    token => new NegateNode()),
                new LookbackLiteralParser(
                    AdditionNode.Literal,
                    previous => previous is OperatorNode,
                    token => null), // we basically strip out non-additive + operators
                // Values
                new NumericParser(token => token.Contains(".") ? new DoubleValueNode(token) : (ExpressionNode)new IntValueNode(token)),
                new VariableParser((token) => new VariableNode(token)),
                // Structural
                new SimpleLiteralParser(OpenBracketNode.Literal, token => new OpenBracketNode()),
                new SimpleLiteralParser(CloseBracketNode.Literal, token => new CloseBracketNode())
            };
        }

        private readonly IReadOnlyCollection<IParser> _parsers;

        public ParserProvider(IEnumerable<IParser> parsers = null)
        {
            _parsers = (parsers ?? DefaultParsers).ToList();
        }

        public IReadOnlyCollection<IParser> Get()
        {
            return _parsers;
        }
    }
}
