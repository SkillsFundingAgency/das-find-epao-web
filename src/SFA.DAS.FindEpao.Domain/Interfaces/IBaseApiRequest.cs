using Newtonsoft.Json;

namespace SFA.DAS.FindEpao.Domain.Interfaces
{
    public interface IBaseApiRequest
    {
        [JsonIgnore]
        string BaseUrl { get; }
    }
}
