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

## Usage

wip!