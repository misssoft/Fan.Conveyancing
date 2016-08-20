using System.Linq;
using NUnit.Framework;

namespace StampDutyCalculator.Tests
{
    [TestFixture]
    class StampDutyCalculatorTests
    {
        [TestCase(95000.0, 0.0)]
        [TestCase(150000.0, 500.0)]
        [TestCase(300000.0, 5000.0)]
        [TestCase(1000000.0, 43750.0)]
        [TestCase(2000000.0, 153750.0)]
        [TestCase(3000000.0, 273750.0)]
        public void StampDutyCalculator_Calculates_NormalRate(double purchasePrice, double expectedTax)
        {
            //arrange
            var calculator = new Fan.StampDuty.StampDutyCalculator(false);
            //act
            var result = calculator.CalculateStampDuty(purchasePrice);
            var total = result.Sum();
            //assert
            Assert.AreEqual(expectedTax, total);
        }

        [TestCase(95000.0, 2850.0)]
        [TestCase(150000.0, 5000.0)]
        [TestCase(300000.0, 14000.0)]
        [TestCase(1000000.0, 73750.0)]
        [TestCase(2000000.0, 213750.0)]
        [TestCase(3000000.0, 363750.0)]
        public void StampDutyCalculator_Calculates_HigherRate(double purchasePrice, double expectedTax)
        {
            //arrange
            var calculator = new Fan.StampDuty.StampDutyCalculator(true);
            //act
            var result = calculator.CalculateStampDuty(purchasePrice);
            var total = result.Sum();
            //assert
            Assert.AreEqual(expectedTax, total);
        }

        [TestCase(95000.0, 0.0)]
        [TestCase(150000.0, 500.0)]
        [TestCase(300000.0, 5000.0)]
        [TestCase(1000000.0, 43750.0)]
        [TestCase(2000000.0, 153750.0)]
        [TestCase(3000000.0, 273750.0)]
        public void CalculateFullStampDuty_Calculates_NormalRate(double purchasePrice, double expectedTax)
        {
            //arrange
            var calculator = new Fan.StampDuty.StampDutyCalculator(false);
            //act
            var result = calculator.CalculateFullStampDuty(purchasePrice);
            var total = result.Sum(x => x.Tax);
            //assert
            Assert.AreEqual(expectedTax, total);

        }

        [TestCase(95000.0, 2850.0)]
        [TestCase(150000.0, 5000.0)]
        [TestCase(300000.0, 14000.0)]
        [TestCase(1000000.0, 73750.0)]
        [TestCase(2000000.0, 213750.0)]
        [TestCase(3000000.0, 363750.0)]
        public void CalculateFullStampDuty_Calculates_HighRate(double purchasePrice, double expectedTax)
        {
            //arrange
            var calculator = new Fan.StampDuty.StampDutyCalculator(true);
            //act
            var result = calculator.CalculateFullStampDuty(purchasePrice);
            var total = result.Sum(x => x.Tax);
            //assert
            Assert.AreEqual(expectedTax, total);

        }
    }
}
