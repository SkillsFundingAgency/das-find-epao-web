using Newtonsoft.Json;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class EpaoDetails
    {
        [JsonProperty("id")]
        public string EpaoId { get; set; }
        public string Name { get; set; }
        public string PrimaryContactName { get; set; }
        [JsonProperty("organisationData")]
        public EpaoContactDetails ContactDetails { get; set; }
    }
}
