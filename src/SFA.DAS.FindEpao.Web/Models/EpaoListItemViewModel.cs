using System.Collections.Generic;
using System.Linq;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class EpaoListItemViewModel
    {
        public EpaoListItemViewModel(EpaoListItem epao, IReadOnlyList<DeliveryArea> deliveryAreas)
        {
            EpaoId = epao.EpaoId;
            Name = epao.Name;
            City = epao.City;
            Postcode = epao.Postcode;
            Locations = BuildLocations(epao, deliveryAreas);
        }

        public string EpaoId { get; }
        public string Name { get; }
        public string City { get; }
        public string Postcode { get; }
        public string Locations { get; }
        public string Address => $"{City}, {Postcode}";
        
        private string BuildLocations(EpaoListItem epao, IReadOnlyList<DeliveryArea> deliveryAreas)
        {
            var hashedEpaoDeliveryAreas = epao.DeliveryAreas
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
                .Where(area => epao.DeliveryAreas.Any(deliveryArea => deliveryArea.DeliveryAreaId == area.Id))
                .OrderBy(area => area.Ordering)
                .Select(area => area.Area));

            return locations;
        }
    }
}
