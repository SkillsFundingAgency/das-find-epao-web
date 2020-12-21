using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models.EpaoDetailsViewModelTests
{
    public class WhenGettingHasAddress
    {
        [Test]
        public void And_Address_Fields_Then_False()
        {
            var contactDetails = new EpaoContactDetailsViewModel();

            contactDetails.HasAddress().Should().BeFalse();
        }

        [Test, AutoData]
        public void And_Has_Address1_Then_True(
            string address1)
        {
            var contactDetails = new EpaoContactDetailsViewModel{Address1 = address1};

            contactDetails.HasAddress().Should().BeTrue();
        }

        [Test, AutoData]
        public void And_Has_Address2_Then_True(
            string address2)
        {
            var contactDetails = new EpaoContactDetailsViewModel{Address2 = address2};

            contactDetails.HasAddress().Should().BeTrue();
        }

        [Test, AutoData]
        public void And_Has_Address3_Then_True(
            string address3)
        {
            var contactDetails = new EpaoContactDetailsViewModel{Address3 = address3};

            contactDetails.HasAddress().Should().BeTrue();
        }

        [Test, AutoData]
        public void And_Has_Address4_Then_True(
            string address4)
        {
            var contactDetails = new EpaoContactDetailsViewModel{Address4 = address4};

            contactDetails.HasAddress().Should().BeTrue();
        }

        [Test, AutoData]
        public void And_Has_Postcode_Then_True(
            string postcode)
        {
            var contactDetails = new EpaoContactDetailsViewModel{Postcode = postcode};

            contactDetails.HasAddress().Should().BeTrue();
        }
    }
}
