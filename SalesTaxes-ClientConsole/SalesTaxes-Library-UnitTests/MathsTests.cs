using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxes_Library;
using System.Globalization;

namespace SalesTaxes_Library_UnitTests
{
    [TestClass]
    public class MathsTests
    {
        [DataTestMethod]
        [DataRow("12.00", "12.00")]

        [DataRow("12.01", "12.05")]
        [DataRow("12.02", "12.05")]
        [DataRow("12.03", "12.05")]
        [DataRow("12.04", "12.05")]
        [DataRow("12.05", "12.05")]

        [DataRow("12.06", "12.10")]
        [DataRow("12.07", "12.10")]
        [DataRow("12.08", "12.10")]
        [DataRow("12.09", "12.10")]
        public void RoundUpToTheNearest005_Should_Round_Correctly(
            string valueToBeRoundedString,
            string roundedResultString)
        {
            // I had to pass it as strings because with this test framework
            // it is forbidden to pass a decimal as test parameter. Sadly.
            decimal valueToBeRounded = decimal.Parse(valueToBeRoundedString, CultureInfo.InvariantCulture);
            decimal roundedResult = decimal.Parse(roundedResultString, CultureInfo.InvariantCulture);

            Assert.AreEqual(valueToBeRounded.RoundUpToTheNearest005(), roundedResult);
        }
    }
}
