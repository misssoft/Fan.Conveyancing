using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fan.Conveyancing
{
    public enum ConveyancingType
    {
        Select= 0,
        Sale = 1,
        Purchase = 2,
        SaleAndPurchase = 3,
        Remortgage = 4
    }

    public class Range<TValue> where TValue : IComparable<TValue>
    {
        public TValue Min { get; set; }
        public TValue Max { get; set; }
        public double Value { get; set; }

        public Range(TValue min, TValue max, double value)
        {
            this.Min = min;
            this.Max = max;
            this.Value = value;
        }
    }

    public class StampDutySettings
    {
        public double[] MinValue { get; set; }
        public double[] MaxValue { get; set; }
        public double[] Percentage { get; set; }
    }

}
