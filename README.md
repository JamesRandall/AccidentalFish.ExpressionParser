# AccidentalFish.ExpressionParser

## Introduction

This project makes use of Dijkstra's shunting yard algorithm to take an expression expressed as a string (e.g. "5+9*2") and
convert it to an abstract syntax tree that can be used for further purposes. I've spun this code out of another project where
I am using it to:

* Generate terms for Elastic Search queries
* Generate SQL sub queries (in a scenario where it was not possible to use LINQ)

I've also bundled with this a simple converter that takes the expression tree, converts it to a LINQ expression tree, and compiles an
appropriate lambda. Numerous solutions exist for doing this already (Flee, DynamincLinq) and so this was largely written to facilitate
testing via expression evaluation and just, to be honest, for interest.

## Basic Usage

The simplest way to build an expression tree is to use a static method as follows:

    ExpressionNode rootNode = ExpressionFactory.Split("5+9-3");

To use it via an IoC container a dependency registration class is supplied:

    Dependencies.Register((interface, impl) => myContainer.Register(interface, impl));
	IExpressionFactory factory = myContainer.Resolve<IExpressionFactory>();
	ExpressionNode rootNode = factory.ParseExpression("5+9+3");

To convert the above expression tree into a LINQ expression (and then compile it) use code such as the following:

    Expression expression = LinqExpressionTreeBuilder.BuildLinq(rootNode);
	Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(expression);
	Func<int> func = lambda.Compile();
	int result = func();

Again the LINQ builder can be used via dependency injection and a complete example of that would look like:

    AccidentalFish.ExpressionParser.Dependencies.Register((interface, impl) => myContainer.Register(interface, impl));
	AccidentalFish.ExpressionParser.LinqDependencies.Register((interface, impl) => myContainer.Register(interface, impl));
	IExpressionFactory factory = myContainer.Resolve<IExpressionFactory>();
	ILinqExpressionTreeBuilder linqExpressionTreeBuilder = myContainer.Resolve<ILinqExpressionTreeBuilder>();
	ExpressionNode rootNode = factory.ParseExpression("5+9+3");
	Expression expression = linqExpressionTreeBuilder.Build(rootNode);
	Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(expression);
	Func<int> func = lambda.Compile();
	int result = func();

Additional samples can be found in the unit tests.

## Expression Nodes



## Tests

Just a quick note about the tests - they're a mix of unit and integration developed in a vaguely TDD sense and in a manner
that helped me get from start to something working as quickly as possible.