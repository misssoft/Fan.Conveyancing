using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fan.Conveyancing
{
    public static class LookupHelper
    {
        public static List<Range<double>> _legalFeeTable = new List<Range<double>>
        {
            new Range<double>(0.0, 125000.0, 0),
            new Range<double>(125001.0, 200000, 100),
            new Range<double>(200001.0, 300000.0, 200),
            new Range<double>(300001.0, 400000.0, 300),
            new Range<double>(400001.0, 500000.0, 400),
            new Range<double>(500001.0, 1000000.0, 500),
            new Range<double>(1000000.0, double.MaxValue, 10000)
        };

        public static List<Range<double>> _landRegistrationTable = new List<Range<double>>
        {
            new Range<double>(0.0, 80000.0, 10),
            new Range<double>(80001.0, 100000.0, 20),
            new Range<double>(100001.0, 200000.0, 30),
            new Range<double>(200001.0, 500000.0, 40),
            new Range<double>(500001.0, 1000000.0, 50),
            new Range<double>(1000000.0, double.MaxValue, 500)
        };

        public static List<Range<double>> _remortgageFeeTable = new List<Range<double>>
        {
            new Range<double>(1.0, 500000.0, 100),
            new Range<double>(500001.0,double.MaxValue, 1000)
        }; 


        public static double LegalFee(double price)
        {
            var result = 0.0;

            if (price <= 0) return result;

            var range = _legalFeeTable.Find((x => x.Min <= price && x.Max >= price));
            result = range.Value;
            return result;
        }

        public static double LandRegistrationFee(double price)
        {
            var result = 0.0;

            if (price <= 0) return result;

            var range = _landRegistrationTable.Find((x => x.Min <= price && x.Max >= price));
            result = range.Value;
            return result;
        }

        public static double RemortgageFee(double price)
        {
            var result = 0.0;

            if (price <= 0) return result;

            var range = _remortgageFeeTable.Find(x => x.Min <= price && x.Max >= price);
            result = range.Value;
            return result;
        }

    }
}


