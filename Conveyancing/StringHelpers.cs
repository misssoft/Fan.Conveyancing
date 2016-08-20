using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fan.Conveyancing
{
    public static class StringHelpers
    {
        public static string AppSetting(this string Key)
        {
            string ret = "0";
            if (System.Configuration.ConfigurationManager.AppSettings[Key] != null)
                ret = ConfigurationManager.AppSettings[Key];
            return ret;
        }
    }

}
