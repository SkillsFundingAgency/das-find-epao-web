using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models.EpaoListItemViewModelTests
{
    public class WhenGettingLocations
    {
        [Test]
        public void And_All_Areas_Then_Return_NationalCoverage()
        {
            var epao = new EpaoListItem
            {
                DeliveryAreas = new List<EpaoDeliveryArea>
                {
                    new EpaoDeliveryArea {DeliveryAreaId = 1},
                    new EpaoDeliveryArea {DeliveryAreaId = 2},
                    new EpaoDeliveryArea {DeliveryAreaId = 3},
                    new EpaoDeliveryArea {DeliveryAreaId = 4}
                }
            };

            var model = new EpaoListItemViewModel(epao, DeliveryAreas);

            model.Locations.Should().Be("National coverage");
        }

        [Test]
        public void And_Not_All_Areas_Then_Returns_Area_Names_In_Order()
        {
            var epao = new EpaoListItem{
                DeliveryAreas = new List<EpaoDeliveryArea>
                {
                    new EpaoDeliveryArea {DeliveryAreaId = 1},
                    new EpaoDeliveryArea {DeliveryAreaId = 2},
                    new EpaoDeliveryArea {DeliveryAreaId = 3}
                }
            };
            var expectedLocation = string.Join(", ", DeliveryAreas
                .Where(area => epao.DeliveryAreas.Any(deliveryArea => deliveryArea.DeliveryAreaId == area.Id))
                .OrderBy(area => area.Ordering)
                .Select(area => area.Area));

            var model = new EpaoListItemViewModel(epao, DeliveryAreas);

            model.Locations.Should().Be(expectedLocation);
        }

        private static readonly List<DeliveryArea> DeliveryAreas = new List<DeliveryArea>
        {
            new DeliveryArea{ Id = 1, Area = "Area 1", Ordering = 3},
            new DeliveryArea{ Id = 2, Area = "Area 2", Ordering = 2},
            new DeliveryArea{ Id = 3, Area = "Area 3", Ordering = 1},
            new DeliveryArea{ Id = 4, Area = "Area 4", Ordering = 4}
        };
    }
}
