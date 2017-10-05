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
        private readonly IReadOnlyCollection<IParser> _parsers;

        public ParserProvider()
        {
            _parsers = new List<IParser>
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
                new LookbackLiteralParser(AdditionOperator.Literal, // we use a lookback operator to deal with negation and positive syntax e.g. 5+-3
                    (previous) => !(previous is OperatorNode),
                    token => new AdditionOperator()),
                new LookbackLiteralParser(SubtractionOperator.Literal,
                    (previous) => !(previous is OperatorNode),
                    token => new SubtractionOperator()),
                new SimpleLiteralParser(MultiplicationOperator.Literal, token => new MultiplicationOperator()),
                new SimpleLiteralParser(DivisionOperator.Literal, token => new DivisionOperator()),
                new LookbackLiteralParser(
                    NegateOperatorNode.Literal,
                    previous => previous is OperatorNode,
                    token => new NegateOperatorNode()),
                new LookbackLiteralParser(
                    AdditionOperator.Literal,
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

        public ParserProvider(IEnumerable<IParser> parsers)
        {
            _parsers = parsers.ToList();
        }

        public IReadOnlyCollection<IParser> Get()
        {
            return _parsers;
        }
    }
}
