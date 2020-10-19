using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class EpaoListItemViewModel
    {
        public string EpaoId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public IEnumerable<DeliveryArea> DeliveryAreas { get; set; }
        public string Locations { get; set; } //needs some logic
        public string Address => $"{City}, {Postcode}";

        public static implicit operator EpaoListItemViewModel(EpaoListItem epao)
        {
            return new EpaoListItemViewModel
            {
                EpaoId = epao.EpaoId,
                Name = epao.Name,
                City = epao.City,
                Postcode = epao.Postcode,
                DeliveryAreas = epao.DeliveryAreas
            };
        }
    }
}
