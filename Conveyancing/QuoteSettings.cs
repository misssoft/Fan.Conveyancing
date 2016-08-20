using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fan.Conveyancing
{
    public class QuoteSettings
    {
        public ConveyancingType Type { get; set; }
    
        public double Price { get; set; }

        public string LocationofPurchase {
            get { return "England and Wales"; }
        }

        [DisplayName("Is the transaction in joint names?")]
        public bool IsJointName { get; set; }

        [DisplayName("Is the property freehold?")]
        public bool IsLeasehold { get; set; }
        
        public bool Mortgaged { get; set; }

        [DisplayName("Is the property a buy to let/second home?")]
        public bool IsSecondHome { get; set; }

        [DisplayName("Are you a Staff Member?")]
        public bool IsStaff { get; set; }

        [DisplayName("Are you a Member?")]
        public bool IsMember { get; set; }

        [DisplayName("Is any transfer of equity required?")]
        public bool IsTransferEquity { get; set; }

        public QuoteSettings(ConveyancingType type)
        {
            Type = type;
        }

        public QuoteSettings()
        {
            Type = ConveyancingType.Select;
        }
    }

}
