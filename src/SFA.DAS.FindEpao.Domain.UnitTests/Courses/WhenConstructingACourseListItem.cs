using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Courses
{
    public class WhenConstructingACourseListItem
    {
        [Test]
        public void WhenConstructorParametersArePassed_ThenTheCourseSetsTheValues()
        {
            //Arrange
            var expectedCourseId = "1-21-1";
            var expectedCourseTitle = "Course 1";
            var expectedCourseLevel = 4;
            var expectedIntegratedApprenticeship = true;
            var expectedVersions = new string[] { "1.0" };

            //Act
            var actual = new CourseListItem(expectedCourseId, expectedCourseTitle, expectedCourseLevel, expectedIntegratedApprenticeship, expectedVersions);

            //Assert
            Assert.AreEqual(expectedCourseId, actual.Id);
            Assert.AreEqual(expectedCourseTitle, actual.Title);
            Assert.AreEqual(expectedCourseLevel, actual.Level);
            Assert.AreEqual(expectedIntegratedApprenticeship, actual.IntegratedApprenticeship);
            Assert.AreEqual(expectedVersions, actual.StandardVersions);
        }


        [TestCase(null)]
        public void WhenStandardVersionsAreNull_ReturnEmptyArray(string[] expectedVersions)
        {
            //Act
            var course = new CourseListItem("", "Course 1", 3, true, expectedVersions);

            //Assert
            Assert.IsNull(course.StandardVersions);

        }


        [TestCase("")]
        [TestCase(null)]
        public void WhenCourseTitleIsEmptyOrNull_SetCourseTitleToUnknown(string expectedTitle)
        {
            //Arrange
            var expectedString = "Unknown";

            //Act
            var course = new CourseListItem("",expectedTitle,3, true, new string[] { "1.0" });

            //Assert
            Assert.AreEqual(expectedString, course.Description);
            Assert.AreEqual(expectedString, course.Title);

        }

        [Test]
        public void Then_The_Course_Description_Is_Taken_From_The_Title_And_Level()
        {
            //Arrange Act
            var actualApprenticeship = new CourseListItem("", "Some title", 1, false, new string[] { "1.0" });

            //Assert
            Assert.AreEqual("Some title (level 1)", actualApprenticeship.Description);
        }

        [Test]
        public void Then_The_Empty_Constructor_Sets_The_Title_To_Unknown()
        {
            //Arrange
            var expectedString = "Unknown";

            //Act
            var course = new CourseListItem(null, null, 0, false, new string[] { "1.0" });

            //Assert
            Assert.AreEqual(expectedString, course.Description);
        }
    }
}
