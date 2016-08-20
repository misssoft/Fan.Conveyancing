namespace Fan.Conveyancing
{
    public class QuoteResult
    {
        private readonly ConveyancingType _type;
        private readonly double _discountRate;
        private double _discountAmount;
        private double _legalFeeTotal;
        private double _total;
        private double _price;

        public QuoteResult(ConveyancingType type, double price)
        {
            _discountRate = 0.05;
            _type = type;
            _price = price;
        }

        public QuoteResult(double discountRate)
        {
            _discountRate = discountRate;
        }
        public double LegalFee { get; set; }

        public double TelegraphicTransferFee { get; set; }

        public double LeaseholdSupplement { get; set; }

        public double MortgageAdminFee { get; set; }

        public double SearchFee { get; set; }

        public double StampDuty { get; set; }

        public double LandRegistration { get; set; }

        public bool ApplyDiscount { get; set; }

        public double GetPrice()
        {
            return _price;
        }

        public double GetDiscount()
        {
            if (ApplyDiscount)
            {
                if (_type == ConveyancingType.Remortgage)
                {
                    _discountAmount = (LegalFee + LeaseholdSupplement)*(0 - _discountRate);
                }
                else
                {
                    _discountAmount = (LegalFee + TelegraphicTransferFee + LeaseholdSupplement) * (0 - _discountRate);    
                }
                
            }
            else _discountAmount = 0;
            return _discountAmount;
        }

        public double GetLegalFeeTotalWithDiscount()
        {
            if (ApplyDiscount)
                _legalFeeTotal = (LegalFee + TelegraphicTransferFee + LeaseholdSupplement) * (1 - _discountRate);
            else _legalFeeTotal = LegalFee + TelegraphicTransferFee + LeaseholdSupplement;
            return _legalFeeTotal;
        }

        public ConveyancingType GetQuoteType()
        {
            return _type;
        }

        public double GetTotal()
        {
            switch (_type)
            {
                case ConveyancingType.Purchase:
                    _total = GetTotalPurchase();
                    break;
                case ConveyancingType.Sale:
                    _total = GetTotalSale();
                    break;
                case ConveyancingType.Remortgage:
                    _total = GetTotalRemorgage();
                    break;
            }
            return _total;
        }

        private double GetTotalPurchase()
        {
            _total = GetLegalFeeTotalWithDiscount() + StampDuty + SearchFee + LandRegistration;
            return _total;
        }

        private double GetTotalSale()
        {
            _total = GetLegalFeeTotalWithDiscount();
            return _total;
        }

        private double GetTotalRemorgage()
        {
            _total = LegalFee + LeaseholdSupplement + GetDiscount();
            return _total;
        }
    }
}
