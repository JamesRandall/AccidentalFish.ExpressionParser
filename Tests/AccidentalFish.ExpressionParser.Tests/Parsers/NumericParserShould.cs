using System;
using System.Collections.Generic;
using System.Text;
using AccidentalFish.ExpressionParser.Parsers;
using Xunit;

namespace AccidentalFish.ExpressionParser.Tests.Parsers
{
    
    public class NumericParserShould
    {
        [Fact]
        public void ReturnTrueForIntegerPartialMatch()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("5", null);

            Assert.True(result);
        }

        [Fact]
        public void ReturnFalseForIntegerPartialMatchWithOperator()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("5+", null);

            Assert.False(result);
        }

        [Fact]
        public void ReturnFalseForIntegerWithSign()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("+5", null);

            Assert.False(result);
        }

        [Fact]
        public void ReturnFalseForIntegerPartialMatchWithParameterDelimiter()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("5,", null);

            Assert.False(result);
        }

        [Fact]
        public void ReturnTrueForDoublePartialMatch()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("5.5", null);

            Assert.True(result);
        }

        [Fact]
        public void ReturnFalseForDoublePartialMatchWithOperator()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("5.5+", null);

            Assert.False(result);
        }

        [Fact]
        public void ReturnFalseForDoubleWithSign()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("+5.5", null);

            Assert.False(result);
        }

        [Fact]
        public void ReturnFalseForDoublePartialMatchWithParameterDelimiter()
        {
            NumericParser testSubject = new NumericParser(null);
            bool result = testSubject.IsPartialMatch("5.5,", null);

            Assert.False(result);
        }
    }
}
