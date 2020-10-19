using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class EpaoListItem
    {
        public string EpaoId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public IEnumerable<DeliveryArea> DeliveryAreas { get; set; }
    }

    public class DeliveryArea
    {
        public int DeliveryAreaId { get; set; }
    }
}
