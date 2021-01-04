using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
using SFA.DAS.FindEpao.Web.Infrastructure.Interfaces;
using SFA.DAS.FindEpao.Web.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Web.UnitTests.Controllers.CoursesControllerTests
{
    public class WhenGettingCourseEpaos
    {
        [Test, MoqAutoData]
        public async Task And_Model_Invalid_Redirect_To_Not_Found(
            GetCourseEpaosRequest getRequest,
            ValidationException exception,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseEpaosQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var result = await controller.CourseEpaos(getRequest) as RedirectToRouteResult;

            result.RouteName.Should().Be(RouteNames.Error404);
        }

        [Test, MoqAutoData]
        public async Task Then_Gets_Epaos_From_Handler(
            GetCourseEpaosRequest getRequest,
            GetCourseEpaosResult mediatorResult,
            [Frozen] Mock<ILocationStringBuilder> mockLocationStringBuilder,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseEpaosQuery>(query => query.CourseId == getRequest.Id), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var result = await controller.CourseEpaos(getRequest) as ViewResult;

            var model = result.Model as CourseEpaosViewModel;
            model.Course.Should().BeEquivalentTo((CourseListItemViewModel)mediatorResult.Course);
            model.Epaos.Should().BeEquivalentTo(mediatorResult.Epaos.Select(item => new EpaoListItemViewModel(
                item, 
                mediatorResult.DeliveryAreas, 
                mockLocationStringBuilder.Object.BuildLocationString)));
        }

        [Test, MoqAutoData]
        public async Task Then_Has_No_Epaos_Then_Adds_Empty_List(
            GetCourseEpaosRequest getRequest,
            GetCourseEpaosResult mediatorResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mediatorResult.Epaos = new List<EpaoListItem>();
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseEpaosQuery>(query => query.CourseId == getRequest.Id), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var result = await controller.CourseEpaos(getRequest) as ViewResult;

            var model = result.Model as CourseEpaosViewModel;
            model.Course.Should().BeEquivalentTo((CourseListItemViewModel)mediatorResult.Course);
            model.Epaos.Should().BeEmpty();
        }
    }
}
