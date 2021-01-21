using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Exceptions;
using SFA.DAS.FindEpao.Web.Controllers;
using SFA.DAS.FindEpao.Web.Infrastructure;
using SFA.DAS.FindEpao.Web.Infrastructure.Interfaces;
using SFA.DAS.FindEpao.Web.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Web.UnitTests.Controllers.CoursesControllerTests
{
    public class WhenGettingCourseEpao
    {
        [Test, MoqAutoData]
        public async Task And_Model_Invalid_Redirect_To_Epao_Unavailable(
            GetCourseEpaoDetailsRequest getRequest,
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

            result!.RouteName.Should().Be(RouteNames.ErrorEpaoUnavailable);
        }

        [Test, MoqAutoData]
        public async Task And_NotFoundException_Then_Redirect_To_Epao_Unavailable(
            GetCourseEpaoDetailsRequest getRequest,
            NotFoundException<CourseEpao> exception,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseEpaoQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var result = await controller.CourseEpao(getRequest) as RedirectToRouteResult;

            result!.RouteName.Should().Be(RouteNames.ErrorEpaoUnavailable);
        }

        [Test, MoqAutoData]
        public async Task Then_Gets_CourseEpao_From_Handler(
            GetCourseEpaoDetailsRequest getRequest,
            GetCourseEpaoResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Frozen] Mock<ILocationStringBuilder> mockLocationStringBuilder,
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
            model!.CourseEpaosCount.Should().Be(mediatorResult.CourseEpaosCount);
            model!.Epao.Should().BeEquivalentTo(new EpaoDetailsViewModel(
                mediatorResult.Epao, 
                mediatorResult.EpaoDeliveryAreas,
                mediatorResult.DeliveryAreas,
                mockLocationStringBuilder.Object.BuildLocationString,
                getRequest.Single
                ));
        }
    }
}
