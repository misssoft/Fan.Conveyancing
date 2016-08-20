using NUnit.Framework;
using NUnit.Framework.Constraints;
using Fan.StampDuty;

namespace Fan.Conveyancing.Tests
{
    [TestFixture]
    public class ConveyancingCalculatorTests
    {
        [Test]
        public void Calculator_Constructed_withAppSettingValues()
        {
            //arrange
            var calculator = new ConveyancingCalculator(new StampDutyCalculator());
            //act
            //assert
            Assert.IsInstanceOf<ConveyancingCalculator>(calculator);
        }

        [Test]
        public void Calculator_CalculatePurchase_FirstHome_DefaultSettings()
        {
            //arrange
            var settings = new QuoteSettings(ConveyancingType.Purchase)
            {
                Price = 95000,
            };
            //act
            var calculator = new ConveyancingCalculator(new StampDutyCalculator());
            var result = calculator.GetQuoteForPurchase(settings);
            
            //assert
            Assert.AreEqual(20.0,result.GetTotal());          
        }

        [Test]
        public void Calculator_CalculatePurchase_FirstHome_HasMortgage_IsLeaseHold_AddFees()
        {
            //arrange
            var settings = new QuoteSettings(ConveyancingType.Purchase)
            {
                Price = 95000,
                Mortgaged = true,
                IsLeasehold = true
            };
            //act
            var calculator = new ConveyancingCalculator(new StampDutyCalculator());
            var result = calculator.GetQuoteForPurchase(settings);

            //assert
            Assert.AreEqual(20.0, result.GetTotal());
        }

        [TestCase(true, true, 20.0)]
        [TestCase(false, true, 20.0)]
        [TestCase(true, false, 20.0)]
        [TestCase(false,false, 20.0)]
        public void Calculator_CalculatePurchase_FirstHome_HasMortgage_IsLeaseHold_isMemberorDiscount_AddFees(bool member, bool staff, double quote)
        {
            //arrange
            var settings = new QuoteSettings(ConveyancingType.Purchase)
            {
                Price = 95000,
                Mortgaged = true,
                IsLeasehold = true,
                IsMember = member,
                IsStaff = staff,
            };
            //act
            var calculator = new ConveyancingCalculator(new StampDutyCalculator());
            var result = calculator.GetQuoteForPurchase(settings);

            //assert
            Assert.AreEqual(quote, result.GetTotal());
        }
        
        [TestCase(false, 95000.0, 0.0, 20.0)]
        [TestCase(false, 150000.0, 500.0, 630.0)]
        [TestCase(false, 300000.0, 5000.0, 5240.0)]
        [TestCase(false, 1000000.0, 43750.0, 44300.0)]
        [TestCase(false, 2000000.0, 153750.0, 164250.0)]
        [TestCase(false, 3000000.0, 273750.0, 284250.0)]
        [TestCase(true, 95000.0, 2850.0, 2870.0)]
        [TestCase(true, 150000.0, 5000.0, 5130)]
        [TestCase(true, 300000.0, 14000.0, 14240.0)]
        [TestCase(true, 1000000.0, 73750.0, 74300.0)]
        [TestCase(true, 2000000.0, 213750.0, 224250.0)]
        [TestCase(true, 3000000.0, 363750.0, 374250.0)]
        public void Calculator_CalculatePurchase_HasMortgage_IsLeaseHold_WithStampDuty(bool secondhome, double purchaseprice, double stampDuty, double quote )
        {
            //arrange
            var settings = new QuoteSettings(ConveyancingType.Purchase)
            {
                Price = purchaseprice,
                Mortgaged = true,
                IsLeasehold = true,
                IsSecondHome = secondhome
            };
            var expected = new QuoteResult(ConveyancingType.Purchase, settings.Price)
            {
                LegalFee = LookupHelper.LegalFee(purchaseprice),
                TelegraphicTransferFee = 42,
                LeaseholdSupplement = 180,
                MortgageAdminFee = 0,
                SearchFee = 299,
                StampDuty = stampDuty,  // Highrate
                LandRegistration = 40,
            };
            //act
            var calculator = new ConveyancingCalculator(new StampDutyCalculator(secondhome));
            var result = calculator.GetQuoteForPurchase(settings);

            //assert
            Assert.AreEqual(expected.StampDuty, result.StampDuty);
            Assert.AreEqual(quote, result.GetTotal());
        }

        [TestCase(95000, false, false, true, 0.0)]
        [TestCase(95000, false, true, false, 0.0)]
        [TestCase(95000, true, false, false, 0.0)]
        [TestCase(95000, false, false, false, 0.0)]
        public void Calculator_CalculateSale(double salePrice, bool isLeaseHold, bool isMember, bool isStaff, double expectedQuote)
        {
            //arrange
            var settings = new QuoteSettings(ConveyancingType.Sale)
            {
                Price = salePrice,
                IsLeasehold = isLeaseHold,
                IsMember = isMember,
                IsStaff = isStaff
            };
            //act
            var calculator = new ConveyancingCalculator(new StampDutyCalculator());
            var result = calculator.GetQuoteForPurchase(settings);

            //assert
            Assert.AreEqual(expectedQuote, result.GetTotal());
        }

        [TestCase(95000, 95000, false, false, false, false, 0.0, 20.00)]
        [TestCase(95000, 95000, true, false, false, false, 0.0, 20.00)]
        [TestCase(95000, 95000, true, true, false, false,  0.0, 20.00)]
        [TestCase(150000, 150000, false, false, false, false, 200.0, 730.0)]
        [TestCase(300000, 150000, false, false, false, false, 300.0, 5340.00)]
        [TestCase(250000, 300000, false, false, false, false, 400.0, 2940.0)]
        public void Calculator_CalculatePurchaseandSale(double purchasePrice, double salePrice,  bool isSaleLeaseHold,  bool isPurchaseLeaseHold, bool isMember, bool isStaff, double expectedLegalFee, double expectedQuote)
        {
            //arrange
            var purchaseSettings = new QuoteSettings(ConveyancingType.Purchase)
            {
                Price = purchasePrice,
                IsLeasehold = isPurchaseLeaseHold,
                IsMember = isMember,
                IsStaff = isStaff
            };

            var saleseSettings = new QuoteSettings(ConveyancingType.Purchase)
            {
                Price = salePrice,
                IsLeasehold = isSaleLeaseHold,
                IsMember = isMember,
                IsStaff = isStaff
            };
            //act
            var calculator = new ConveyancingCalculator(new StampDutyCalculator());
            var purchaseResult = calculator.GetQuoteForPurchase(purchaseSettings);
            var saleResult = calculator.GetQuoteForSale(saleseSettings);

            //assert
            Assert.AreEqual(expectedLegalFee, purchaseResult.GetLegalFeeTotalWithDiscount() + saleResult.GetLegalFeeTotalWithDiscount());
            Assert.AreEqual(expectedQuote, purchaseResult.GetTotal() + saleResult.GetTotal());

        }

        [TestCase(1500001.0, false, false, false,1000.0)]
        [TestCase(500001.0, false, false, false, 1000.0)]
        [TestCase(500000.0, false, false, false, 100.0)]
        [TestCase(35000.0, true, true, true, 95.0)]
        [TestCase(35000.0, true, false, true, 95.0)]
        [TestCase(35000.0, true, true, false, 95.0)]
        [TestCase(35000.0, true, false, false, 100.0)]
        [TestCase(35000.0, false, false, false, 100.0)]
        [TestCase(0.0, false, false, false, 0.0)]
        public void Calculator_CalculateRemortgage(double mortgagePrice,  bool leasehold, bool staff, bool member, double expectedQuote)
        {
            //arrange
            var remortgageSettings = new QuoteSettings(ConveyancingType.Remortgage)
            {
                Price = mortgagePrice,
                IsLeasehold = leasehold,
                IsStaff = staff,
                IsMember =  member
            };
            //act
            var calculator = new ConveyancingCalculator(new StampDutyCalculator());
            var remortgageResult = calculator.GetQuoteForRemortgage(remortgageSettings);
            //assert

            Assert.AreEqual(expectedQuote, remortgageResult.GetTotal());
        }
    }
}
