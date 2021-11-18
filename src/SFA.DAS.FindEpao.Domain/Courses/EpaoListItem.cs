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

        public List<EpaoStandardsListItem.StandardsListItem> standardVersions { get; set; }

    }

    public class EpaoDeliveryArea
    {
        public int DeliveryAreaId { get; set; }
    }


    public class EpaoStandardsListItem
    {
        public EpaoStandardsListItem()
        {
            standardVersions = new List<StandardsListItem>();
        }

        public string Id { get; set; }
        public string organisationId { get; set; }
        public int standardCode { get; set; }
        public string standardReference { get; set; }
        public DateTime? dateStandardApprovedOnRegister { get; set; }
        public DateTime? effectiveFrom { get; set; }
        public DateTime? effectiveTo { get; set; }
        public List<StandardsListItem> standardVersions { get; set; }


        public class StandardsListItem
        {
            public string standardUId { get; set; }
            public string title { get; set; }
            public int larsCode { get; set; }
            public string version { get; set; }
            public DateTime? effectiveFrom { get; set; }
            public DateTime? effectiveTo { get; set; }
            public DateTime? dateVersionApproved { get; set; }
            public string status { get; set; }
        }

    }
}
