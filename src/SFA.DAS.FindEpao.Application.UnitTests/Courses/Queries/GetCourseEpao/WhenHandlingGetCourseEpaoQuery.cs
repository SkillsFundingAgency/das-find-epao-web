using System;
using System.ComponentModel.DataAnnotations;
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
using SFA.DAS.FindEpao.Domain.Validation;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.FindEpao.Domain.Validation.ValidationResult;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourseEpao
{
    public class WhenHandlingGetCourseEpaoQuery
    {
        [Test, MoqAutoData]
        public async Task And_Query_InValid_Then_Throws_ValidationException(
            GetCourseEpaoQuery query,
            string propertyName,
            ValidationResult validationResult,
            [Frozen] Mock<IValidator<GetCourseEpaoQuery>> mockValidator,
            GetCourseEpaoQueryHandler handler)
        {
            validationResult.AddError(propertyName);
            mockValidator
                .Setup(validator => validator.ValidateAsync(query))
                .ReturnsAsync(validationResult);

            var act = new Func<Task>(async () => await handler.Handle(query, CancellationToken.None));

            act.Should().Throw<ValidationException>()
                .WithMessage($"*{propertyName}*");
        }

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
