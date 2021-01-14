using System;
using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class EpaoListItemViewModel : EpaoWithLocation
    {
        public EpaoListItemViewModel(
            EpaoListItem epao, 
            IReadOnlyList<DeliveryArea> deliveryAreas, 
            Func<IReadOnlyList<EpaoDeliveryArea>, IReadOnlyList<DeliveryArea>, string> buildLocations) 
            : base(epao.DeliveryAreas, deliveryAreas, buildLocations)
        {
            EpaoId = epao.EpaoId;
            Name = epao.Name;
            City = epao.City;
            Postcode = epao.Postcode;
            EffectiveFrom = epao.EffectiveFrom;
        }

        public string EpaoId { get; }
        public string Name { get; }
        public string City { get; }
        public string Postcode { get; }
        public string Address => $"{City}, {Postcode}";
        public DateTime? EffectiveFrom { get; set; }
    }
}
