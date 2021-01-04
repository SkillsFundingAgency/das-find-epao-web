using System.Collections.Generic;
using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Web.Services;

namespace SFA.DAS.FindEpao.Web.UnitTests.Services
{
    public class WhenBuildingLocationString
    {
        [Test, AutoData]
        public void And_All_Areas_Then_Return_NationalCoverage(LocationStringBuilderService builder)
        {
            var epaoDeliveryAreas = new List<EpaoDeliveryArea>
            {
                new EpaoDeliveryArea {DeliveryAreaId = 1},
                new EpaoDeliveryArea {DeliveryAreaId = 2},
                new EpaoDeliveryArea {DeliveryAreaId = 3},
                new EpaoDeliveryArea {DeliveryAreaId = 4}
            };

            var result = builder.BuildLocationString(epaoDeliveryAreas, DeliveryAreas);

            result.Should().Be("National coverage");
        }

        [Test, AutoData]
        public void And_Not_All_Areas_Then_Returns_Area_Names_In_Order(LocationStringBuilderService builder)
        {
            var epaoDeliveryAreas = new List<EpaoDeliveryArea>
            {
                new EpaoDeliveryArea {DeliveryAreaId = 1},
                new EpaoDeliveryArea {DeliveryAreaId = 2},
                new EpaoDeliveryArea {DeliveryAreaId = 3}
            };
            var expectedLocation = string.Join(", ", DeliveryAreas
                .Where(area => epaoDeliveryAreas.Any(deliveryArea => deliveryArea.DeliveryAreaId == area.Id))
                .OrderBy(area => area.Ordering)
                .Select(area => area.Area));

            var result = builder.BuildLocationString(epaoDeliveryAreas, DeliveryAreas);

            result.Should().Be(expectedLocation);
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
