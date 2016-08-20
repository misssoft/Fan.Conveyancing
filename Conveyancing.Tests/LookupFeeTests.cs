using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace Fan.Conveyancing.Tests
{
    /// <summary>
    /// Summary description for LookupFeeTests
    /// </summary>
    [TestFixture]
    public class LookupFeeTests
    {
        [TestCase(1000001.0, 10000.0)]
        [TestCase(1000000.0, 500.0)]
        [TestCase(500001.0, 500.0)]
        [TestCase(500000.0, 400.0)]
        [TestCase(400001.0, 400.0)]
        [TestCase(400000.0, 300.0)]
        [TestCase(300001.0, 300.0)]
        [TestCase(300000.0, 200.0)]
        [TestCase(200001.0, 200.0)]
        [TestCase(200000.0, 100.0)]
        [TestCase(125001.0, 100.0)]
        [TestCase(125000.0,0.0)]
        [TestCase(50.0, 0.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(-50.0, 0.0)]
        public void Lookup_LegalFeeTable_ReturnCorrectValue(double input, double output)
        {
            //arrange
            //act
            var result = LookupHelper.LegalFee(input);
            //assert
            Assert.AreEqual(output, result);
        }

        [TestCase(1000001.0, 500.0)]
        [TestCase(1000000.0, 50.0)]
        [TestCase(500001.0, 50.0)]
        [TestCase(500000.0, 40.0)]
        [TestCase(200001.0, 40.0)]
        [TestCase(200000.0, 30.0)]
        [TestCase(100001.0, 30.0)]
        [TestCase(100000.0, 20.0)]
        [TestCase(80000.0, 10.0)]
        [TestCase(50.0, 10.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(-50.0, 0.0)]
        public void Lookup_LandRegistrationTable_ReturnCorrectValue(double input, double output)
        {
            //arrange
            //act
            var result = LookupHelper.LandRegistrationFee(input);
            //assert
            Assert.AreEqual(output, result);
        }

        [TestCase(1500001.0, 1000.0)]
        [TestCase(500001.0, 1000.0)]
        [TestCase(500000.0, 100.0)]
        [TestCase(35000.0, 100.0)]
        [TestCase(0.0, 0.0)]
        public void Lookup_RemortgageFeeTable_ReturnCorrectValue(double input, double output)
        {
            //arrange
            //act
            var result = LookupHelper.RemortgageFee(input);
            //assert
            Assert.AreEqual(output, result);
        }

    }
}
