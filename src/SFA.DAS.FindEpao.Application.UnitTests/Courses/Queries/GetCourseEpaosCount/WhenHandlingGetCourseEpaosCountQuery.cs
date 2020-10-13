using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaosCount;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Domain.Validation;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.FindEpao.Domain.Validation.ValidationResult;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourseEpaosCount
{
    public class WhenHandlingGetCourseEpaosCountQuery
    {
        [Test, MoqAutoData]
        public async Task And_Query_InValid_Then_Throws_ValidationException(
            GetCourseEpaosCountQuery query,
            string propertyName,
            ValidationResult validationResult,
            [Frozen] Mock<IValidator<GetCourseEpaosCountQuery>> mockValidator,
            GetCourseEpaosCountQueryHandler handler)
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
            GetCourseEpaosCountQuery query,
            int courseEpaosCount,
            [Frozen] Mock<IValidator<GetCourseEpaosCountQuery>> mockValidator,
            [Frozen] Mock<ICourseService> mockCourseService,
            GetCourseEpaosCountQueryHandler handler)
        {
            mockValidator
                .Setup(validator => validator.ValidateAsync(query))
                .ReturnsAsync(new ValidationResult());
            mockCourseService
                .Setup(service => service.GetCourseEpaosCount(query.CourseId))
                .ReturnsAsync(courseEpaosCount);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Total.Should().Be(courseEpaosCount);
        }
    }
}
