using System;
using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Web.Models
{
    public abstract class EpaoWithLocation
    {
        public EpaoWithLocation(
            IReadOnlyList<EpaoDeliveryArea> epaoDeliveryAreas, 
            IReadOnlyList<DeliveryArea> deliveryAreas, 
            Func<IReadOnlyList<EpaoDeliveryArea>, IReadOnlyList<DeliveryArea>, string> buildLocations)
        {
            Locations = buildLocations(epaoDeliveryAreas, deliveryAreas);
        }

        public string Locations { get; }
    }
}
