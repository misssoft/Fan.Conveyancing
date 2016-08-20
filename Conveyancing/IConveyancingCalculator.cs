using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fan.Conveyancing
{
    public interface IConveyancingCalculator
    {
        QuoteResult GetQuoteForPurchase( QuoteSettings settings);
        QuoteResult GetQuoteForSale(QuoteSettings settings);
        QuoteResult GetQuoteForRemortgage(QuoteSettings settings);
    }
}
