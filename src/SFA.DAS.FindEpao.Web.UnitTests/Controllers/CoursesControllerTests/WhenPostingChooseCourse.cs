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
    }
}
