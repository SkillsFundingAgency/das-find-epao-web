using System.Collections.Generic;
using System.Linq;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Web.Infrastructure.Interfaces;

namespace SFA.DAS.FindEpao.Web.Services
{
    public class LocationStringBuilderService : ILocationStringBuilder
    {
        public string BuildLocationString(IReadOnlyList<EpaoDeliveryArea> epaoDeliveryAreas, IReadOnlyList<DeliveryArea> deliveryAreas)
        {
            var hashedEpaoDeliveryAreas = epaoDeliveryAreas
                .Select(area => area.DeliveryAreaId)
                .ToHashSet();

            var hashedDeliveryAreas = deliveryAreas
                .Select(area => area.Id)
                .ToHashSet();

            if (hashedEpaoDeliveryAreas.SetEquals(hashedDeliveryAreas))
            {
                return "National coverage";
            }

            var locations = string.Join(", ", deliveryAreas
                .Where(area => epaoDeliveryAreas.Any(deliveryArea => deliveryArea.DeliveryAreaId == area.Id))
                .OrderBy(area => area.Ordering)
                .Select(area => area.Area));

            return locations;
        }
    }
}
