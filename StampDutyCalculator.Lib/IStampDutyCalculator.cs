using System.Collections.Generic;

namespace Fan.StampDuty
{
    public interface IStampDutyCalculator
    {
        double[] CalculateStampDuty(double price);
        IEnumerable<StampDutyBand> CalculateFullStampDuty(double price);

    }
}