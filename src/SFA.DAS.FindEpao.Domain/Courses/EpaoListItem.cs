using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class EpaoListItem
    {
        //id, title, locations, address, all england
        [JsonProperty("endPointAssessorOrganisationId")]
        public string EpaoId { get; set; }
        [JsonProperty("endPointAssessorName")]
        public string Name { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        [JsonProperty("deliveryAreasDetails")]
        public IEnumerable<DeliveryArea> DeliveryAreas { get; set; }
    }

    public class DeliveryArea
    {
        public int Id { get; set; }
        public int DeliveryAreaId { get; set; }
        [JsonProperty("deliveryArea")]
        public string Name { get; set; }
    }
}
