using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models.CourseEpaosViewModelTests
{
    public class WhenGettingTotal
    {
        [Test, AutoData]
        public void And_Null_Epaos_Then_Returns_0(CourseEpaosViewModel model)
        {
            model.Epaos = null;
            model.Total.Should().Be(0);
        }

        [Test, AutoData]
        public void Then_Returns_Epao_Count(CourseEpaosViewModel model)
        {
            model.Total.Should().Be(model.Epaos.Count());
        }
    }
}
