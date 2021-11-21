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
        public DateTime EffectiveFrom { get; set; }

        public List<EpaoStandardsListItem> StandardVersions { get; set; }

    }

    public class EpaoDeliveryArea
    {
        public int DeliveryAreaId { get; set; }
    }


    public class EpaoStandardsListItem
    {
        public string StandardUId { get; set; }
        public string Title { get; set; }
        public int LarsCode { get; set; }
        public string Version { get; set; }
        public int Level { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? DateVersionApproved { get; set; }
        public string Status { get; set; }

    }
}
