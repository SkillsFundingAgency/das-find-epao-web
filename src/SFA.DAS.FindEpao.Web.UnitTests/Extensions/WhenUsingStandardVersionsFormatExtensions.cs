using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;
using SFA.DAS.FindEpao.Web.Extensions;

namespace SFA.DAS.FindEpao.Web.UnitTests.Extensions
{
    [TestFixture]
    public class WhenUsingStandardVersionsFormatExtensions
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



    }
}
