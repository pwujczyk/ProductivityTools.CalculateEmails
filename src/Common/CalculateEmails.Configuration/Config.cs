using MasterConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.Configuration
{
    public class Config :IConfig
    {
        public string MQAdress
        {
            get
            {
                var address = $"net.msmq://{MConfiguration.Configuration["ServerName"]}/private/{MConfiguration.Configuration["QueneName"]}";
                return address;
            }
        }

        public string OnlineAddress
        {
            get
            {
                var address = $"http://{MConfiguration.Configuration["ServerName"]}:{MConfiguration.Configuration["port"]}";
                return address;
            }
        }

         public string GetSqlServerConnectionString()
        {
            return ConnectionStringLightPT.ConnectionStringLight.GetSqlServerConnectionString(GetSqlDataSource(), GetSqlServerDataBaseName());
        }


        public string GetDataSourceConnectionString()
        {
            return ConnectionStringLightPT.ConnectionStringLight.GetSqlDataSourceConnectionString(GetSqlDataSource());
        }

        public string GetSqlDataSource()
        {
            return @".\Sql2017";
        }

        public string GetSqlServerDataBaseName()
        {
            return @"EcoVadisPT";
        }
    }
}
