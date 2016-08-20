using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fan.StampDuty
{
    public class StampDutyCalculator : IStampDutyCalculator 
    {
        public StampDutyBand[] Bands { get; }

        public StampDutyCalculator(bool hightax = false)
        {
            if (hightax)
            {
                Bands = new StampDutyBand[5]{
                     new StampDutyBand() {MinValue = 0.0, MaxValue = 125000.0, Percentage=0.03  },
                     new StampDutyBand() {MinValue = 125000.0, MaxValue = 250000, Percentage=0.05},
                     new StampDutyBand() {MinValue = 250000.0, MaxValue = 925000.0, Percentage=0.08 },
                     new StampDutyBand() {MinValue = 925000.0, MaxValue = 1500000.0, Percentage=0.13 },
                     new StampDutyBand() {MinValue = 1500000.0, MaxValue = double.MaxValue, Percentage=0.15 }};
            }
            else
            {
                Bands = new StampDutyBand[5]{
                     new StampDutyBand() {MinValue = 0.0, MaxValue = 125000.0, Percentage=0.0 },
                     new StampDutyBand() {MinValue = 125000.0, MaxValue = 250000, Percentage=0.02 },
                     new StampDutyBand() {MinValue = 250000.0, MaxValue = 925000.0, Percentage=0.05 },
                     new StampDutyBand() {MinValue = 925000.0, MaxValue = 1500000.0, Percentage=0.10 },
                     new StampDutyBand() {MinValue = 1500000.0, MaxValue = double.MaxValue, Percentage=0.12 }};
            }
        }

        public double[] CalculateStampDuty(double price)
        {
            double[] result = new double[5] { 0.0, 0.0, 0.0, 0.0, 0.0 };

            if (price <= 0) return result;

            var index = 0;

            while (price > 0)
            {
                var band = Bands[index];

                var taxbleSum = band.MaxValue - band.MinValue;
                
                if (price > taxbleSum)
                {
                    result[index] = taxbleSum * band.Percentage;
                }
                else
                {
                    result[index] = price * band.Percentage;
                }
                price = price - taxbleSum;
                index++;
            }
            return result;
           }

        public IEnumerable<StampDutyBand> CalculateFullStampDuty(double price)
        {
            List<StampDutyBand> payBands = new List<StampDutyBand>();

            var bands = Bands.Where(x => x.MinValue < price).ToList();

            for (int i=0; i<bands.Count; i++)
            {
                var band = bands[i];

                if (price<band.MaxValue)
                {
                    band.PayableSum = price - band.MinValue;
                }
                else
                {
                    band.PayableSum = band.MaxValue - band.MinValue;
                }

                payBands.Add(band);
            }

            return payBands;
        }
    }
    }
