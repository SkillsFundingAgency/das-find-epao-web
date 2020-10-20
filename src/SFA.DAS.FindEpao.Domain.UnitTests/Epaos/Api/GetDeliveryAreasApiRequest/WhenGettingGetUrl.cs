using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Epaos.Api.GetDeliveryAreasApiRequest
{
    public class WhenGettingGetUrl
    {
        [Test, AutoData]
        public void Then_Returns_Correct_Path(Domain.Epaos.Api.GetDeliveryAreasApiRequest request)
        {
            request.GetUrl.Should().Be("epaos/delivery-areas");
        }
    }
}
