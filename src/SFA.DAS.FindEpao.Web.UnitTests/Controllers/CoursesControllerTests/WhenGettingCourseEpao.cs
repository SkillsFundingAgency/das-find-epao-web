using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao;
using SFA.DAS.FindEpao.Web.Controllers;
using SFA.DAS.FindEpao.Web.Infrastructure;
using SFA.DAS.FindEpao.Web.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Web.UnitTests.Controllers.CoursesControllerTests
{
    public class WhenGettingCourseEpao
    {
        [Test, MoqAutoData]
        public async Task And_Model_Invalid_Redirect_To_Not_Found(
            GetCourseEpaoRequest getRequest,
            ValidationException exception,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseEpaoQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var result = await controller.CourseEpao(getRequest) as RedirectToRouteResult;

            result!.RouteName.Should().Be(RouteNames.Error404);
        }

        [Test, MoqAutoData]
        public async Task And_NotFoundException_Then_Redirect_To_Not_Found(
            GetCourseEpaoRequest getRequest,
            HttpRequestException exception,//todo: notfoundexception
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseEpaoQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var result = await controller.CourseEpao(getRequest) as RedirectToRouteResult;

            result!.RouteName.Should().Be(RouteNames.Error404);
        }

        [Test, MoqAutoData]
        public async Task Then_Gets_CourseEpao_From_Handler(
            GetCourseEpaoRequest getRequest,
            GetCourseEpaoResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseEpaoQuery>(query => 
                        query.CourseId == getRequest.Id
                        && query.EpaoId == getRequest.EpaoId), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var result = await controller.CourseEpao(getRequest) as ViewResult;

            var model = result!.Model as CourseEpaoViewModel;
            model!.Course.Should().BeEquivalentTo((CourseListItemViewModel)mediatorResult.Course);
            model!.Epao.Should().BeEquivalentTo(new EpaoListItemViewModel(mediatorResult.Epao, mediatorResult.DeliveryAreas));
        }
    }
}
