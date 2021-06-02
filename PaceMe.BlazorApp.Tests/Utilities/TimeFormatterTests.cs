using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaceMe.BlazorApp.Utilities;

namespace PaceMe.BlazorApp.Tests.Utilities
{
    [TestClass]
    public class TimeFormatterTests
    {
        [TestMethod]
        [DataRow(30, "0:30")]
        [DataRow(59, "0:59")]
        [DataRow(60, "1:00")]
        [DataRow(599, "9:59")]
        [DataRow(600, "10:00")]
        [DataRow(615, "10:15")]
        [DataRow(3599, "59:59")]
        [DataRow(3600, "1:00:00")]
        [DataRow(3601, "1:00:01")]
        [DataRow(3661, "1:01:01")]
        [DataRow(36061, "10:01:01")]

        public void GivenTimeInSeconds_FormatsTimeToString(int inputSeconds, string expectedOutput)
        {
            Assert.AreEqual(expectedOutput, TimeFormatter.SecondsToTime(inputSeconds));
        }
    }
}
