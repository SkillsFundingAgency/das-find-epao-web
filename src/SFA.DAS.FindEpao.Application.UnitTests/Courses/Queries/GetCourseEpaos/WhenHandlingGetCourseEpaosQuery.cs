using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Domain.Validation;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.FindEpao.Domain.Validation.ValidationResult;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourseEpaos
{
    public class WhenHandlingGetCourseEpaosQuery
    {
        [Test, MoqAutoData]
        public void And_Query_InValid_Then_Throws_ValidationException(
            GetCourseEpaosQuery query,
            string propertyName,
            ValidationResult validationResult,
            [Frozen] Mock<IValidator<GetCourseEpaosQuery>> mockValidator,
            GetCourseEpaosQueryHandler handler)
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
        public async Task Then_Gets_CourseEpaos_From_Service(
            GetCourseEpaosQuery query,
            CourseEpaos courseEpaos,
            DeliveryAreaList deliveryAreas,
            [Frozen] Mock<IValidator<GetCourseEpaosQuery>> mockValidator,
            [Frozen] Mock<ICourseService> mockCourseService,
            [Frozen] Mock<IEpaoService> mockEpaoService,
            GetCourseEpaosQueryHandler handler)
        {
            mockValidator
                .Setup(validator => validator.ValidateAsync(query))
                .ReturnsAsync(new ValidationResult());
            mockCourseService
                .Setup(service => service.GetCourseEpaos(query.CourseId))
                .ReturnsAsync(courseEpaos);
            mockEpaoService
                .Setup(service => service.GetDeliveryAreas())
                .ReturnsAsync(deliveryAreas);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Course.Should().BeEquivalentTo(courseEpaos.Course);
            result.Epaos.Should().BeEquivalentTo(courseEpaos.Epaos);
            result.DeliveryAreas.Should().BeEquivalentTo(deliveryAreas.DeliveryAreas);
        }
    }
}
