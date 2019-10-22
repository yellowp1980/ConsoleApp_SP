using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SP.Services
{
    public static class Common
    {
        public static string GetAppSetting(string key)
        {
            string val = "";
            val = ConfigurationSettings.AppSettings[key];


            if (string.IsNullOrEmpty(val))
                throw new Exception("Unable to locate value or entry for AppSetting" + key);

            return val;
        }
    }
}
