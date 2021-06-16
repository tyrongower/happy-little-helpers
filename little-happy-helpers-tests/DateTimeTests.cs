using System;
using NUnit.Framework;

namespace little_happy_helpers_tests
{
    public class DateTimeTests
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void  ToStartOfMonthTest()
        {
            var testDate = new DateTime(2021, 01, 15, 23, 55, 15);

            var withTime = testDate.ToStartOfMonth(true);
            var withoutTime = testDate.ToStartOfMonth();
            
            Assert.True(new DateTime(2021, 01, 1, 23, 55, 15) == withTime,"With time");
            Assert.True(new DateTime(2021, 01, 1) == withoutTime,"Without time");
        }
    }
}