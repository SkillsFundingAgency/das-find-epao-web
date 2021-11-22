using System;
using System.Collections.Generic;
using System.Linq;
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
            StandardVersions = epao.StandardVersions;
            Versions = GetVersions(StandardVersions);
        }

        public string EpaoId { get; }
        public string Name { get; }
        public string City { get; set; }
        public string Postcode { get; }
        public string Address => FormatAddress();
        public DateTime EffectiveFrom { get; set; }
        public List<EpaoStandardsListItem> StandardVersions {get; set;}

        public string Versions { get; set; }

        private string FormatAddress()
        {
            if (string.IsNullOrEmpty(City))
            {
                return Postcode;
            }

            return $"{City}, {Postcode}";
        }
    
        private string GetVersions(List<EpaoStandardsListItem> standardVersions)
        {
            if (standardVersions != null)
                return string.Join(", ", standardVersions.Select(x => x.Version));
            else
                return string.Empty;
        }

    }
}
