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


            //Act
            var actual = new CourseListItem (expectedCourseId, expectedCourseTitle,expectedCourseLevel, expectedIntegratedApprenticeship);

            //Assert
            Assert.AreEqual(expectedCourseId, actual.Id);
            Assert.AreEqual(expectedCourseTitle, actual.Title);
            Assert.AreEqual(expectedCourseLevel, actual.Level);
            Assert.AreEqual(expectedIntegratedApprenticeship, actual.IntegratedApprenticeship);
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenCourseTitleIsEmptyOrNull_SetCourseTitleToUnknown(string expectedTitle)
        {
            //Arrange
            var expectedString = "Unknown";

            //Act
            var course = new CourseListItem("",expectedTitle,3, true);

            //Assert
            Assert.AreEqual(expectedString, course.Description);
            Assert.AreEqual(expectedString, course.Title);

        }

        [Test]
        public void Then_The_Course_Description_Is_Taken_From_The_Title_And_Level()
        {
            //Arrange Act
            var actualApprenticeship = new CourseListItem("", "Some title", 1, false);

            //Assert
            Assert.AreEqual("Some title (level 1)", actualApprenticeship.Description);
        }

        [Test]
        public void Then_The_Empty_Constructor_Sets_The_Title_To_Unknown()
        {
            //Arrange
            var expectedString = "Unknown";

            //Act
            var course = new CourseListItem(null, null, 0, false);

            //Assert
            Assert.AreEqual(expectedString, course.Description);
        }
    }
}
