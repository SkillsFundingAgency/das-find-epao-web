using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourses;
using SFA.DAS.FindEpao.Web.Controllers;
using SFA.DAS.FindEpao.Web.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Web.UnitTests.Controllers.CoursesControllerTests
{
    public class WhenGettingChooseCourse
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Courses_From_Mediator(
            GetCoursesResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCoursesQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var response = await controller.ChooseCourse() as ViewResult;

            var model = response.Model as ChooseCourseViewModel;
            model.Courses.Should().BeEquivalentTo(
                mediatorResult.Courses.Select(item => (CourseListItemViewModel)item));
        }

        [Test, MoqAutoData]
        public async Task And_Invalid_Then_Adds_ModelState_Error(
            GetChooseCourseRequest request,
            GetCoursesResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCoursesQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            await controller.ChooseCourse(request);

            controller.ModelState.IsValid.Should().BeFalse();
            controller.ModelState.ContainsKey("SelectedCourseId").Should().BeTrue();
            controller.ModelState.ContainsKey("SelectedCourseIdNoJs").Should().BeTrue();
            controller.ModelState["SelectedCourseId"].Errors[0].ErrorMessage
                .Should().Be("Enter an apprenticeship training course");
            controller.ModelState["SelectedCourseIdNoJs"].Errors[0].ErrorMessage
                .Should().Be("Select an apprenticeship training course");
        }
    }
}
