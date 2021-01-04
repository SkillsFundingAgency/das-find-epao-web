using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Web.Infrastructure.Interfaces
{
    public interface ILocationStringBuilder
    {
        string BuildLocationString(IReadOnlyList<EpaoDeliveryArea> epaoDeliveryAreas, IReadOnlyList<DeliveryArea> deliveryAreas);
    }
}
