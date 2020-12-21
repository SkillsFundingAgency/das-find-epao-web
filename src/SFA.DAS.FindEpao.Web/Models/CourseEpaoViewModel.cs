using System;
using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class CourseEpaoViewModel
    {
        public CourseListItemViewModel Course { set; get; }
        public EpaoDetailsViewModel Epao { get; set; }
        public int CourseEpaosCount { get; set; }
    }

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

    public class EpaoContactDetailsViewModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteLink { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Postcode { get; set; }

        public static implicit operator EpaoContactDetailsViewModel(
            EpaoContactDetails source)
        {
            if (source is null) return default;

            return new EpaoContactDetailsViewModel
            {
                Email = source.Email,
                PhoneNumber = source.PhoneNumber,
                WebsiteLink = source.WebsiteLink,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                Address4 = source.Address4,
                Postcode = source.Postcode
            };
        }
    }
}
