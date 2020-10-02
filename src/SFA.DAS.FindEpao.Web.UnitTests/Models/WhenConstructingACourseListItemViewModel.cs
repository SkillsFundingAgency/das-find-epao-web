﻿using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.UnitTests.Models
{
    public class WhenConstructingACourseListItemViewModel
    {
        [Test, AutoData]
        public void Then_Sets_Id(
            CourseListItem source)
        {
            CourseListItemViewModel viewModel = source;
            
            viewModel.Should().BeEquivalentTo(source);
        }
    }
}
