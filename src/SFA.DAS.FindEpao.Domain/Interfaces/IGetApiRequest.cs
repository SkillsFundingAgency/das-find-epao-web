using Newtonsoft.Json;

namespace SFA.DAS.FindEpao.Domain.Interfaces
{
    public interface IGetApiRequest : IBaseApiRequest
    {
        [JsonIgnore]
        string GetUrl { get; }
    }
}
