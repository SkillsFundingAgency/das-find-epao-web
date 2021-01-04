using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourse;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Web.Controllers;
using SFA.DAS.FindEpao.Web.Infrastructure;
using SFA.DAS.FindEpao.Web.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Web.UnitTests.Controllers.CoursesControllerTests
{
    public class WhenGettingIntegratedApprenticeshipCourse
    {
        [Test, MoqAutoData]
        public async Task Then_The_IntegratedApprenticeshipCourse_Is_Returned(
            GetIntegratedApprenticeshipCourseRequest request,
            GetCourseResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mediatorResult.Course = new CourseListItem(mediatorResult.Course.Id, mediatorResult.Course.Title,
                mediatorResult.Course.Level, true);
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseQuery>(c=>c.CourseId.Equals(request.Id)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var actual = await controller.CourseIntegrated(request) as ViewResult;

            var model = actual.Model as IntegratedApprenticeshipCourseViewModel;
            Assert.IsNotNull(model);
            model.Course.Should().BeEquivalentTo(mediatorResult.Course);
        }

        [Test, MoqAutoData]
        public async Task Then_If_The_Course_Is_Not_Integrated_Then_Page_Not_Found_Is_Returned(
            GetIntegratedApprenticeshipCourseRequest request,
            GetCourseResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mediatorResult.Course = new CourseListItem(mediatorResult.Course.Id, mediatorResult.Course.Title,
                mediatorResult.Course.Level, false);
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseQuery>(c=>c.CourseId.Equals(request.Id)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var actual = await controller.CourseIntegrated(request) as RedirectToRouteResult;
            
            actual.RouteName.Should().Be(RouteNames.Error404);
        }

        [Test, MoqAutoData]
        public async Task Then_If_The_Course_Is_Not_Found_Then_Page_Not_Found_Is_Returned(
            GetIntegratedApprenticeshipCourseRequest request,
            GetCourseResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mediatorResult.Course = null;
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseQuery>(c=>c.CourseId.Equals(request.Id)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var actual = await controller.CourseIntegrated(request) as RedirectToRouteResult;
            
            actual.RouteName.Should().Be(RouteNames.Error404);
        }

        [Test, MoqAutoData]
        public async Task Then_If_There_Is_An_Error_Then_The_Error_Page_Is_Shown(
            GetIntegratedApprenticeshipCourseRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseQuery>(c => c.CourseId.Equals(request.Id)),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Error"));

            var actual = await controller.CourseIntegrated(request) as RedirectToRouteResult;
            
            actual.RouteName.Should().Be(RouteNames.Error500);
        }
    }
}