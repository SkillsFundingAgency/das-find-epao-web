using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Domain.Epaos.Api
{
    public class GetDeliveryAreasApiRequest : IGetApiRequest
    {
        public string GetUrl => "epaos/delivery-areas";
    }
}
