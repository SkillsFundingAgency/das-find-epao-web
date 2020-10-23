using System.Threading.Tasks;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Domain.Interfaces
{
    public interface IEpaoService
    {
        Task<DeliveryAreaList> GetDeliveryAreas();
    }
}
