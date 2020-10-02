using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models
{
    public class WhenConstructingACourseListItemViewModel
    {
        [Test, AutoData]
        public void Then_Sets_Id(
            CourseListItem course)
        {
            var viewModel = new CourseListItemViewModel(course);
            viewModel.Id.Should().Be(course.Id);
        }

        [Test, AutoData]
        public void Then_Sets_Title(
            CourseListItem course)
        {
            var viewModel = new CourseListItemViewModel(course);
            viewModel.Title.Should().Be(course.Title);
        }

        [Test, AutoData]
        public void Then_Sets_Level(
            CourseListItem course)
        {
            var viewModel = new CourseListItemViewModel(course);
            viewModel.Level.Should().Be(course.Level);
        }

        [Test, AutoData]
        public void Then_Sets_Description(
            CourseListItem course)
        {
            var viewModel = new CourseListItemViewModel(course);
            viewModel.Description.Should().Be(course.CourseDescription);
        }

        [Test]
        public void Then_Sets_Selected()
        {
            var course = new CourseListItem("12","Test",1);
            var courseId = course.Id;
            var viewModel = new CourseListItemViewModel(course, courseId);
            viewModel.Selected.Should().Be("selected");
        }

        [Test, AutoData]
        public void And_Not_Match_Course_Then_Selected_Is_Null(
            CourseListItem course)
        {
            var courseId = $"not-{course.Id}";
            var viewModel = new CourseListItemViewModel(course, courseId);
            viewModel.Selected.Should().BeNull();
        }

        [Test, AutoData]
        public void And_Null_CourseId_Then_Selected_Is_Null(
            CourseListItem course)
        {
            var viewModel = new CourseListItemViewModel(course);
            viewModel.Selected.Should().BeNull();
        }

        [Test]
        public void Then_If_The_Course_Is_Null_A_New_ViewModel_Is_Returned_With_Description_Set_To_Unknown()
        {
            var viewModel = new CourseListItemViewModel(null);
            viewModel.Should().NotBeNull();
            viewModel.Description.Should().Be("Unknown");
        }

        [Test]
        public void Then_If_The_Course_Is_An_Empty_Object_The_Select_Is_Null()
        {
            var viewModel = new CourseListItemViewModel(new CourseListItem(null,null,0));
            viewModel.Should().NotBeNull();
            viewModel.Description.Should().Be("Unknown");
            viewModel.Selected.Should().BeNull();
        }
    }
}
