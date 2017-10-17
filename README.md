# AccidentalFish.ExpressionParser

Please note this project is work in progress. It is functional and the API is largely stable but you may trip over work in progress. In particular 
the following are still under development:

	String support
	Linq builder

## Introduction

This project makes use of Dijkstra's shunting yard algorithm to take an expression expressed as a string (e.g. "5+9*2") and
convert it to an abstract syntax tree that can be used for further purposes. I've spun this code out of another project where
I am using it to:

* Generate terms for Elastic Search queries
* Generate query clauses for Cosmos DB queries
* Generate SQL sub queries (in a scenario where it was not possible to use LINQ)

I've also bundled with this a simple converter that takes the expression tree, converts it to a LINQ expression tree, and compiles an
appropriate lambda. Numerous solutions exist for doing this already (Flee, DynamincLinq) and so this was largely written to facilitate
testing via expression evaluation and just, to be honest, for interest.

## Basic Usage

The simplest way to build an expression tree is to first add the parser package via NuGet:

    Install-Package AccidentalFish.ExpressionParser

After doing so a static method called ParseExpression on the ExpressionFactory class allows an
expression expressed as a string to be parsed into a syntax tree:

    ExpressionNode rootNode = ExpressionFactory.Parse("5+9-3");

This will return the root node of a syntax tree representing the expression. To enumerate each node in the
tree a visitor implementation is provided that will walk the tree as an IEnumerable as shown below:

    foreach(ExpressionNode node in new ExpressionTreeVisitor(rootNode)) {
		Console.WriteLine($"Expression node of type {node.GetType().Name}");
	}

More information on the expression nodes themselves can be found below. 

## Using with an IoC Container

The framework supports use through an IoC pattern and appropriate container. To do so
each of the NuGet packages provides a "Dependencies.Register" method (i.e. a static class called Dependencies is exposed that
provides a Register method). This method takes a parameter of type Action<Type, Type> into which you should pass a method that
allows registration of an interface in your container. For example:

    AccidentalFish.ExpressionParser.Dependencies.Register((interface, impl) => myContainer.Register(interface, impl));
	AccidentalFish.ExpressionParser.Linq.Dependencies.Register((interface, impl) => myContainer.Register(interface, impl));
	IExpressionFactory factory = myContainer.Resolve<IExpressionFactory>();
	ILinqExpressionTreeBuilder linqBuilder = myContainer.Resolve<ILinqExpressionTreeBuilder>();
	ExpressionNode rootNode = factory.Parse("5+9+3");
	linqBuilder.Build(rootNode);

## Expression Nodes

All expression nodes ultimately deriver from the abstract ExpressionNode class but are (currently) sub categorised into:

* Operators (deriving from OperatorNode) - addition, subtraction, equality etc.
* Values (deriving from ValueNode) - literal values and variables
* Functions (deriving from FunctionNode) - pow, sin etc.
* Structural (deriving from StructuralNode) - break up expressions and may impact the order of execution

### Operators

|Operator|Type|
|--------|----|
|Addition|AdditionNode|
|Conditional And|ConditionalAndNode|
|Conditional Or|ConditionalOrNode|
|Division|DivisionNode|
|Equality Test|EqualNode|
|Greater Than or Equal To|GreaterThanEqualNode|
|Greater Than|GreaterThanNode|
|Less Than or Equal To|LessThanEqualNode|
|Modulous|ModuloNode|
|Multiplication|MultiplicationNode|
|Negation|NegateNode|
|Not|NotNode|
|Subtraction|SubstractionNode|

Operators are of one of two subtypes, either a binary node wit left and right associated nodes (for example the addition node)
or a unary node with a single associated now (for example a negation node). They are derived from BinaryOperatorNode and UnaryOperatorNode
respectively.

Associativty and precdence is based on C#.

### Values

Values evaluate to either a double, an integer or a string and can be supplied as a literal within the expression or as a variable (a string
preceded by the @ symbol). The example below demonstrates both:

    5+3*@myvar

Variables allow for the values to be retrieved at the time of evaluation or can be useful as placeholders when the expression tree is being used
to translate into a different format.

### Functions

Function nodes derive from the FunctionNode class and when expressed in an expression string take the typical form of:

    functionName(param1,param2)

Built in functions include:

|Function|Type|Example|
|--------|----|-------|
|max|MaxNode|max(5,3)|
|min|MinNode|min(8,3*3)|
|pow|PowNode|pow(2,4)|
|sqrt|SqrtNode|sqrt(16)|

## Compiling Linq Expressions

_Note:_ Please be aware that compiling LINQ expressions was not my original goal for this library and so
there are likely to be dragons here and considerable scope for improvement. I've validated things just enough
to aid my unit testing (being able to compile and evaluate the expression is a nice external way
of ensuring the syntax is as expected, as opposed to the tests self-validating my assumptions about associativity and precedence).

All that said no doubt I will improve this over time.

To build the tree into a LINQ expression, compile, and evaluate it first add the LINQ package from NuGet:

    Install-Package AccidentalFish.ExpressionParser.Linq

Now, continuing with our example, the following code will build and compile the expression and display the output:

    Expression expression = LinqExpressionTreeBuilder.Build(rootNode);
	Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(expression);
	Func<int> func = lambda.Compile();
	int result = func();
	Console.WriteLine($"The expression evaluated as {result}");

Additional samples can be found in the tests.

## Parsing Approach

To get from a string to an expression tree this library takes a 3 step approach:

1. Split the string into an object model collection ordered in the same order in which it is expressed
2. Rearrange the object model collection into reverse polish notation (RPN)
3. Convert the RPN object model collection into a tree and return the root node

Steps 2 and 3 follow a fairly typical [shunting yard](https://en.wikipedia.org/wiki/Shunting-yard_algorithm) approach.

Splitting the expression into an object model works as follows:

1. Begin scanning the string
2. For each scan of the string
3. Build a copy of the list of installed parsers
4. Move forward through the string a character at a time (ignoring whitespace) and removing parsers that don't match and keeping track of the last positive matches
5. When no parsers are left, look at the last set of positive matches and it should contain a single parser - if so invoke the factory and add the resulting node to the list of found nodes. If there is more than one match then the expression syntax has been set up in an ambigous fashion or the expression is faulty so throw an error
6. Backtrack one character and repeat the process scanning the string again until the end of the string is reached

The process is simple and works on the basis that there is no forward looking ambiguity (i.e. the splitter does not need to look ahead to determine if the current token is ambiguous).

## Adding Custom Node Types

To supply a custom set of node types to the expression parser take the following steps:

1. Derive your node from either FunctionNode, OperatorNode or ValueNode as appropriate
2. Decide on a literal representation for your node (for example a function name)
3. Create a parser handler for your new node, you can see examples for the defaults in ParserProvider.cs and add it to the list of supported parsers
4. Crete a ParserProvider from this list and pass it to ExpressionFactory.Parse(). _Note:_ if you're using an IoC container you'll need to register your parser as an IParserProvider implementation.

A simple example is shown below for a random number function:

    List<IParser> parsers = new List<IParser>(ParserProvider.DefaultParsers);
	parsers.Add(new SimpleLiteralParser(RandomNode.Literal, token => new RandomNode()));
	ExpressionNode root = ExpressFactory.Parse("5+random()", parsers);

## Tests

Just a quick note about the tests - they're a mix of unit and integration developed in a vaguely TDD sense and in a manner
that helped me to bootstrap up to something working as quickly as possible.
