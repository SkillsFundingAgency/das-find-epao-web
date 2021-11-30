using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;
using SFA.DAS.FindEpao.Web.Extensions;

namespace SFA.DAS.FindEpao.Web.UnitTests.Extensions
{
    [TestFixture]
    public class WhenUsingDateFormatExtensions
    {
        [TestCase("2021/01/10")]
        [TestCase("01/10/2021")]
        [Test]
        public void Date_Should_Be_Converted_To_Long_Date_Format(DateTime inputDate)
        {
            // Arrange
            string expectedOutputDate = "10 January 2021";

            // Act
            var result = DateFormatExtensions.LongDateFormat(inputDate);

            // Assert
            Assert.AreEqual(result, expectedOutputDate);
        }

        [TestCase(null)]
        [Test]
        public void When_Date_Has_No_Value_Return_Empty_String(DateTime? inputDate)
        {
            // Act
            var result = DateFormatExtensions.LongDateFormat(inputDate);

            // Assert
            Assert.IsEmpty(result);
        }

    }
}
