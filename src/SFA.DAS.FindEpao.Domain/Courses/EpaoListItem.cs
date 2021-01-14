using System;
using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class EpaoListItem
    {
        public string EpaoId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public IReadOnlyList<EpaoDeliveryArea> DeliveryAreas { get; set; }
        public DateTime? EffectiveFrom { get; set; }
    }

    public class EpaoDeliveryArea
    {
        public int DeliveryAreaId { get; set; }
    }
}
