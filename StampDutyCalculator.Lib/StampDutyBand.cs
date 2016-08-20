using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fan.StampDuty;

namespace Fan.StampDuty
{
    public class StampDutyBand
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Percentage { get; set; }
        public double PayableSum { get; set; }
        public double Tax {
            get {
                return PayableSum * Percentage;
            } }

        
     }
}
