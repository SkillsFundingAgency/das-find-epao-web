using System;
using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class EpaoDetailsViewModel : EpaoWithLocation
    {
        public EpaoDetailsViewModel(
            EpaoDetails epao,
            IReadOnlyList<EpaoDeliveryArea> epaoDeliveryAreas, 
            IReadOnlyList<DeliveryArea> deliveryAreas, 
            Func<IReadOnlyList<EpaoDeliveryArea>, IReadOnlyList<DeliveryArea>, string> buildLocations) 
            : base(epaoDeliveryAreas, deliveryAreas, buildLocations)
        {
            EpaoId = epao.EpaoId;
            Name = epao.Name;
            PrimaryContactName = epao.PrimaryContactName;
            ContactDetails = epao.ContactDetails;
        }

        public string EpaoId { get; }
        public string Name { get; }
        public string PrimaryContactName { get; }
        public EpaoContactDetailsViewModel ContactDetails { get; }
    }
}
