using Newtonsoft.Json;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class EpaoListItem
    {
        //id, title, locations, address, all england
        [JsonProperty("endPointAssessorOrganisationId")]
        public string EpaoId { get; set; }
    }
}
