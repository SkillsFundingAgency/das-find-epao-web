using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models
{
    public class WhenCastingAnEpaoListItemViewModelFromDomainType
    {
        [Test, AutoData]
        public void Then_Maps_All_Properties(
            EpaoListItem source)
        {
            EpaoListItemViewModel viewModel = source;
            
            viewModel.Should().BeEquivalentTo(source);
        }
    }
}
