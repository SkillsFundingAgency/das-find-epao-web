using System;
using System.Collections.Generic;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models.EpaoDetailsViewModelTests
{
    public class WhenConstructingAnEpaoDetailsViewModel
    {
        [Test, AutoData]
        public void Then_Maps_All_Properties(
            bool isSingleEpaoResult,
            EpaoDetails source, 
            List<EpaoDeliveryArea> epoaDeliveryAreas,
            List<DeliveryArea> deliveryAreas,
            Func<IReadOnlyList<EpaoDeliveryArea>, IReadOnlyList<DeliveryArea>, string> buildLocations)
        {
            var viewModel = new EpaoDetailsViewModel(source, epoaDeliveryAreas, deliveryAreas, buildLocations, isSingleEpaoResult);
            
            viewModel.Should().BeEquivalentTo(source, config => config.ExcludingMissingMembers());
        }
    }
}
