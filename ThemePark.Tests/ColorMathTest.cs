using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ColoMath;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ThemePark.Tests
{
    [TestFixture]
    [Binding]
    public class ColorMathTest
    {
        private Color color;
        private Color resultColor;

        [Given(@"I have the color red")]
        public void GivenIHaveTheColorRed()
        {
            color = Color.Red;
        }

        [When("I have requested to get the complement")]
        public void WhenIHaveRequestedToGetTheComplement()
        {
            resultColor = color.GetComplement();
        }

        [Then("the result should be the color teal")]
        public void ThenTheResultShouldBeTheColor()
        {
            var complement = Color.FromArgb(255, 0, 255, 255);
            Assert.AreEqual(complement.GetHue(), resultColor.GetHue());
        }


    }
}
