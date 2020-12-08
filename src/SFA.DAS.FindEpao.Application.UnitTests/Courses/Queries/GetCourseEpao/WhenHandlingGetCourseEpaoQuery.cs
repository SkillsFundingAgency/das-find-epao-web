using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourseEpao
{
    public class WhenHandlingGetCourseEpaoQuery
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_CourseEpao_From_Service(
            GetCourseEpaoQuery query,
            CourseEpao courseEpao,
            DeliveryAreaList deliveryAreas,
            [Frozen] Mock<ICourseService> mockCourseService,
            [Frozen] Mock<IEpaoService> mockEpaoService,
            GetCourseEpaoQueryHandler handler)
        {
            mockCourseService
                .Setup(service => service.GetCourseEpao(query.CourseId, query.EpaoId))
                .ReturnsAsync(courseEpao);
            mockEpaoService
                .Setup(service => service.GetDeliveryAreas())
                .ReturnsAsync(deliveryAreas);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Course.Should().BeEquivalentTo(courseEpao.Course);
            result.Epao.Should().BeEquivalentTo(courseEpao.Epao);
            result.DeliveryAreas.Should().BeEquivalentTo(deliveryAreas.DeliveryAreas);
        }
    }
}
