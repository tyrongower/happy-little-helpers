using System;
using System.Collections.Generic;
using System.Linq;
using HappyLittleHelpers.Extensions;
using NUnit.Framework;

namespace little_happy_helpers_tests
{
    public class ConditionalTests
    {
        private class ComplexType
        {
            public int Id { get; private set; }
            public string Value { get; private set; }

            public ComplexType(string value, int id)
            {
                Value = value;
                Id = id;
            }
        }
        
        [Test]
        public void IsInSimpleType()
        {
            var items = new[]
            {
                1,
                2,
                3,
                4,
                5,
                6
            };
            
            Assert.IsTrue(1.IsIn(items));
            Assert.IsFalse(10.IsIn(items));
        }
        
        [Test]
        public void IsInComplexType()
        {
            var testItem = new ComplexType("test3", 3);
            var items = new[]
            {
                new ComplexType("test",1),
                new ComplexType("hello",2),
            }.ToList();
            
            Assert.IsFalse(testItem.IsIn(items));
            
            items.Add(testItem);
            Assert.IsTrue(testItem.IsIn(items));
        }
        

        [Test]
        public void IsInString()
        {
            Assert.True("Hello".IsIn("Hello","Other","Words"),"'Hello'.IsIn('Hello','Other','Words')");
            Assert.False("hello".IsIn("Hello","Other","Words"));
            Assert.True("hello".IsIn(StringComparison.OrdinalIgnoreCase,"Hello","Other","Words"),"'hello'.IsIn(StringComparison.OrdinalIgnoreCase,'Hello','Other','Words')");
        }
    }
}