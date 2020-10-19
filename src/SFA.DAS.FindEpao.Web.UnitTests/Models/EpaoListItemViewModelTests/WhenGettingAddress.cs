using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models.EpaoListItemViewModelTests
{
    public class WhenGettingAddress
    {
        [Test, AutoData]
        public void Then_Formats_Address_Correctly(EpaoListItemViewModel model)
        {
            model.Address.Should().Be($"{model.City}, {model.Postcode}");
        }
    }
}
