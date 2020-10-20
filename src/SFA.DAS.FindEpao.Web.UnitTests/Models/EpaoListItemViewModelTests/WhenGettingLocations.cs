using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models.EpaoListItemViewModelTests
{
    public class WhenGettingLocations
    {
        [Test, Ignore("needs moving elsewhere")]
        public void And_All_Areas_Then_Return_NationalCoverage()
        {
            var model = new EpaoListItemViewModel
            {
                DeliveryAreas = new List<EpaoDeliveryArea>
                {
                    new EpaoDeliveryArea {DeliveryAreaId = 1},
                    new EpaoDeliveryArea {DeliveryAreaId = 2},
                    new EpaoDeliveryArea {DeliveryAreaId = 3},
                    new EpaoDeliveryArea {DeliveryAreaId = 4},
                    new EpaoDeliveryArea {DeliveryAreaId = 5},
                    new EpaoDeliveryArea {DeliveryAreaId = 6},
                    new EpaoDeliveryArea {DeliveryAreaId = 7},
                    new EpaoDeliveryArea {DeliveryAreaId = 8},
                    new EpaoDeliveryArea {DeliveryAreaId = 9}
                }
            };

            model.Locations.Should().Be("National coverage");
        }

        [Test, Ignore("needs moving elsewhere")]
        public void And_Not_All_Areas_Then_Returns_Area_Names_In_Order()
        {
            var model = new EpaoListItemViewModel
            {
                DeliveryAreas = new List<EpaoDeliveryArea>
                {
                    new EpaoDeliveryArea {DeliveryAreaId = 1},
                    new EpaoDeliveryArea {DeliveryAreaId = 2},
                    new EpaoDeliveryArea {DeliveryAreaId = 3}
                }
            };

            //model.Locations.Should().Be(string.Join(", ", ));
        }
    }
}
