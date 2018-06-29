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
        public string QueneName
        {
            get
            {
                return MConfiguration.Configuration["QueneName"];
            }
        }
        public string MQAdress
        {
            get
            {
                //pw: change it
                // var address = $"net.msmq://{MConfiguration.Configuration["ServerName"]}/private/{this.QueneName}";
                var address = $"net.msmq://localhost/private/{this.QueneName}";
                return address;
            }
        }

        public string OnlineAddress
        {
            get
            {
                //pw:change it
                var address = "net.tcp://localhost:5678";
                //var address = $"net.tcp://{MConfiguration.Configuration["ServerName"]}:{MConfiguration.Configuration["Port"]}";
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
