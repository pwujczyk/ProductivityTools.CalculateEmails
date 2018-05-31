using MasterConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public static class Config
    {
        public static string Address
        {
            get
            {
                var address = $"net.msmq://{MConfiguration.Configuration["ServerQueneName"]}/private/{MConfiguration.Configuration["QueneName"]}";
                return address;
            }
        }
    }
}
