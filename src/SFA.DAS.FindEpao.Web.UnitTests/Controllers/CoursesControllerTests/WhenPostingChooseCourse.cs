using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Web.Controllers;
using SFA.DAS.FindEpao.Web.Infrastructure;
using SFA.DAS.FindEpao.Web.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Web.UnitTests.Controllers.CoursesControllerTests
{
    public class WhenPostingChooseCourse
    {
        [Test, MoqAutoData]
        public async Task And_Model_Invalid_Redirect_To_Get_Choose_Course(
            PostChooseCourseRequest postRequest,
            ValidationException exception,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseEpaosQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var result = await controller.PostChooseCourse(postRequest) as RedirectToRouteResult;

            result.RouteName.Should().Be(RouteNames.ChooseCourse);
            result.RouteValues.Should().ContainKey(nameof(GetChooseCourseRequest.SelectedCourseId));
            result.RouteValues[nameof(GetChooseCourseRequest.SelectedCourseId)]
                .Should().Be("-1");
        }

        [Test, MoqAutoData]
        public async Task And_Multiple_Epaos_Then_Redirect_To_CourseEpaos(
            PostChooseCourseRequest postRequest,
            GetCourseEpaosResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseEpaosQuery>(query => query.CourseId == postRequest.SelectedCourseId), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var result = await controller.PostChooseCourse(postRequest) as RedirectToRouteResult;

            result.RouteName.Should().Be(RouteNames.CourseEpaos);
            result.RouteValues.Should().ContainKey("id");
            result.RouteValues["id"].Should().Be(postRequest.SelectedCourseId);
        }

        [Test, MoqAutoData]
        public async Task And_Single_Epao_Then_Redirect_To_CourseEpao(
            PostChooseCourseRequest postRequest,
            GetCourseEpaosResult mediatorResult,
            EpaoListItem foundEpao,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mediatorResult.Epaos = new List<EpaoListItem> {foundEpao};
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseEpaosQuery>(query => query.CourseId == postRequest.SelectedCourseId), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var result = await controller.PostChooseCourse(postRequest) as RedirectToRouteResult;

            result.RouteName.Should().Be(RouteNames.CourseEpaoDetails);
            result.RouteValues.Should().ContainKey("id");
            result.RouteValues["id"].Should().Be(postRequest.SelectedCourseId);
            result.RouteValues.Should().ContainKey("epaoId");
            result.RouteValues["epaoId"].Should().Be(foundEpao.EpaoId);
        }

        [Test, MoqAutoData, Ignore("confirm")]
        public async Task And_Zero_Epao_Then_Redirect_To_(
            PostChooseCourseRequest postRequest,
            GetCourseEpaosResult mediatorResult,
            EpaoListItem foundEpao,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mediatorResult.Epaos = new List<EpaoListItem>();
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseEpaosQuery>(query => query.CourseId == postRequest.SelectedCourseId), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var result = await controller.PostChooseCourse(postRequest) as RedirectToRouteResult;

            result.RouteName.Should().Be(RouteNames.Error404);//???
        }

        [Test, MoqAutoData, Ignore("future story")]
        public async Task And_Epao_Not_AllEngland_Then_Redirect_To_ChooseLocation()
        {

        }
    }
}
