using System.Threading.Tasks;

namespace SFA.DAS.FindEpao.Domain.Interfaces
{
    public interface IApiClient
    {
        Task<TResponse> Get<TResponse>(IGetApiRequest request);
        Task<int> Ping();
    }
}
