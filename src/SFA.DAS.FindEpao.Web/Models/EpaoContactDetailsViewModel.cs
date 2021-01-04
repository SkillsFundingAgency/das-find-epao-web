using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Web.Models
{
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

        public bool HasAddress()
        {
            return !string.IsNullOrEmpty(Address1)
                   || !string.IsNullOrEmpty(Address2)
                   || !string.IsNullOrEmpty(Address3)
                   || !string.IsNullOrEmpty(Address4)
                   || !string.IsNullOrEmpty(Postcode);
        }

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
