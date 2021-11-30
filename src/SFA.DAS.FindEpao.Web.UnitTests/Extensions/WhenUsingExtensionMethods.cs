using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;
using SFA.DAS.FindEpao.Web.Extensions;

namespace SFA.DAS.FindEpao.Web.UnitTests.Extensions
{
    
    [TestFixture]
    public class WhenUsingExtensionMethods
    {

        [Test]
        public void String_Array_Should_Be_Converted_To_Str()
        {
            // Arrange
            string[] inputArray = new string[] { "1.0", "1.1", "1.3" };
            string outputString = "1.0, 1.1, 1.3";

            // Act
            var result = StandardVersionsFormatExtensions.VersionsArrayToString(inputArray);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(outputString, result);
        }

        [Test]
        public void Empty_String_Array_Should_Be_Converted_To_Empty_Str()
        {
            // Arrange
            string[] inputArray = new string[] {  };
            string outputString = "";

            // Act
            var result = StandardVersionsFormatExtensions.VersionsArrayToString(inputArray);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(outputString, result);
        }

        [TestCase("2021/01/10")]
        [TestCase("01/10/2021")]
        [Test]
        public void Date_Should_Be_Converted_To_Long_Date_Format(DateTime inputDate)
        {
            // Arrange
            bool correctDateFormat = false;
            DateTime outputDate;

            // Act
            var result = DateFormatExtensions.LongDateFormat(inputDate);
            if (DateTime.TryParseExact(result, "dd MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDate))
            {
                correctDateFormat = true;
            }

            // Assert
            TestContext.WriteLine(outputDate);
            Assert.IsTrue(correctDateFormat);
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
