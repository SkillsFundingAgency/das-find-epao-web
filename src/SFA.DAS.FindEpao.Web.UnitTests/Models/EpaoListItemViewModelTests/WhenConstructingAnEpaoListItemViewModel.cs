using System;
using System.Collections.Generic;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models.EpaoListItemViewModelTests
{
    public class WhenConstructingAnEpaoListItemViewModel
    {
        [Test, AutoData]
        public void Then_Maps_All_Properties(
            EpaoListItem source, 
            List<DeliveryArea> deliveryAreas,
            Func<IReadOnlyList<EpaoDeliveryArea>, IReadOnlyList<DeliveryArea>, string> buildLocations)
        {
            var viewModel = new EpaoListItemViewModel(source, deliveryAreas, buildLocations);
            
            viewModel.Should().BeEquivalentTo(source, config => config.ExcludingMissingMembers());
        }
    }
}
