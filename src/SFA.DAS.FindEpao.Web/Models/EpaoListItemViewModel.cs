﻿using System;
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
            standardVersions = epao.standardVersions;
            Versions = GetVersions(standardVersions);
        }

        public string EpaoId { get; }
        public string Name { get; }
        public string City { get; set; }
        public string Postcode { get; }
        public string Address => FormatAddress();
        public DateTime EffectiveFrom { get; set; }
        public List<EpaoStandardsListItem.StandardsListItem> standardVersions {get; set;}

        public string Versions { get; set; }

        private string FormatAddress()
        {
            if (string.IsNullOrEmpty(City))
            {
                return Postcode;
            }

            return $"{City}, {Postcode}";
        }
    
        private string GetVersions(List<EpaoStandardsListItem.StandardsListItem> vers)
        {
            string concat = string.Empty;
            int i = 0;

            if (vers != null)
            {
                foreach (var version in vers)
                {
                    if (i == 0)
                    {
                        concat += version.version;
                    }
                    else
                    {
                        concat += ", " + version.version;
                    }

                    i++;
                }
            }
            
            return concat;
        }

    }
}
