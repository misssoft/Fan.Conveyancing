using Fan.StampDuty;
using System;
using System.Configuration;
using System.Linq;

namespace Fan.Conveyancing
{
    public class ConveyancingCalculator: IConveyancingCalculator
    {
        //Sale or Purchase
        private readonly double _telegraphicTransferFee ; 
        private readonly double _leaseholdFee;
        private readonly double _leaseholdFeeRemortgage; 
        private readonly double _mortgageAdminFee;
        private readonly double _propertySearchFee; 
        private readonly IStampDutyCalculator _calculator;

        //Remortgage
        
        public ConveyancingCalculator(IStampDutyCalculator stampDutyCalculator)
        {
            _calculator = stampDutyCalculator;
            var telegraphicTransferFee = "TelegraphicTransferFee".AppSetting();
            _telegraphicTransferFee = Convert.ToDouble(telegraphicTransferFee);
            var leaseholdFee = "LeaseholdFee".AppSetting();
            _leaseholdFee = Convert.ToDouble(leaseholdFee);
            var leaseholdFeeRemortgage = "LeaseholdFeeRemortgage".AppSetting();
            _leaseholdFeeRemortgage = Convert.ToDouble(leaseholdFeeRemortgage);
            var mortgageAdminFee = "MortgageAdminFee".AppSetting();
            _mortgageAdminFee = Convert.ToDouble(mortgageAdminFee);
            var propertySearchFee = "PropertySearchFee".AppSetting();
            _propertySearchFee = Convert.ToDouble(propertySearchFee);
        }
        
        public QuoteResult GetQuoteForPurchase(QuoteSettings purchaSettings)
        {
            if (purchaSettings != null) return CalculatePurchase(purchaSettings);
            return null;
        }

        public QuoteResult GetQuoteForSale(QuoteSettings saleSettings)
        {
            if (saleSettings != null) return CalculateSale(saleSettings);
            return null;
        }

        public QuoteResult GetQuoteForRemortgage(QuoteSettings remortgageSettings)
        {
            if (remortgageSettings != null) return CalculateRemortgage(remortgageSettings);
            return null;
        }

        private QuoteResult CalculateRemortgage(QuoteSettings remortgageSettings)
        {
            if (remortgageSettings.Price <= 0) return new QuoteResult(ConveyancingType.Remortgage, 0);

            var result = new QuoteResult (ConveyancingType.Remortgage, remortgageSettings.Price)
            {
                LegalFee = LookupHelper.RemortgageFee(remortgageSettings.Price),                
            };

            if (remortgageSettings.IsLeasehold) result.LeaseholdSupplement = _leaseholdFeeRemortgage;

            if (remortgageSettings.IsStaff || remortgageSettings.IsMember)
            {
                result.ApplyDiscount = true;
            }
            
            return result;
        }

        private QuoteResult CalculateSale(QuoteSettings settings)
        {
            var result = SetupLegalFeesAndDiscounts(settings);
            return result;
        }

        private QuoteResult CalculatePurchase(QuoteSettings settings)
        {
            var result = SetupLegalFeesAndDiscounts(settings);

            var stampDuty = _calculator.CalculateStampDuty(settings.Price);
            result.StampDuty = stampDuty.Sum();
            result.SearchFee = _propertySearchFee;
            result.LandRegistration = LookupHelper.LandRegistrationFee(settings.Price);

            return result;
        }

        private QuoteResult SetupLegalFeesAndDiscounts(QuoteSettings settings)
        {
            //TODO:Validate the setting;
            var result = new QuoteResult(settings.Type, settings.Price)
            {
                LegalFee = LookupHelper.LegalFee(settings.Price),
                TelegraphicTransferFee = _telegraphicTransferFee
            };

            if (settings.IsLeasehold)
            {
                result.LeaseholdSupplement = _leaseholdFee;
            }

            result.MortgageAdminFee = _mortgageAdminFee;

           
            if (settings.IsStaff || settings.IsMember)
            {
                result.ApplyDiscount = true;
            }
            
            return result;
        }  
    }
}
