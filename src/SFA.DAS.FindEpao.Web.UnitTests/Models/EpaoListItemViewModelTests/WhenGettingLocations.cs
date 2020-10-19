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
                DeliveryAreas = new List<DeliveryArea>
                {
                    new DeliveryArea {DeliveryAreaId = 1},
                    new DeliveryArea {DeliveryAreaId = 2},
                    new DeliveryArea {DeliveryAreaId = 3},
                    new DeliveryArea {DeliveryAreaId = 4},
                    new DeliveryArea {DeliveryAreaId = 5},
                    new DeliveryArea {DeliveryAreaId = 6},
                    new DeliveryArea {DeliveryAreaId = 7},
                    new DeliveryArea {DeliveryAreaId = 8},
                    new DeliveryArea {DeliveryAreaId = 9}
                }
            };

            model.Locations.Should().Be("National coverage");
        }

        [Test, Ignore("needs moving elsewhere")]
        public void And_Not_All_Areas_Then_Returns_Area_Names_In_Order()
        {
            var model = new EpaoListItemViewModel
            {
                DeliveryAreas = new List<DeliveryArea>
                {
                    new DeliveryArea {DeliveryAreaId = 1},
                    new DeliveryArea {DeliveryAreaId = 2},
                    new DeliveryArea {DeliveryAreaId = 3}
                }
            };

            //model.Locations.Should().Be(string.Join(", ", ));
        }
    }
}
