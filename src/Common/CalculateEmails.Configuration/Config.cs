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
               
                var address = $"net.msmq://{ServerName}/private/{this.QueneName}";
                return address;
            }
        }

        private string ServerName
        {
            get
            {
                var serverName = MConfiguration.Configuration["ServerName"];
                return serverName;
            }
        }
        private string Port
        {
            get
            {
                var port =  MConfiguration.Configuration["Port"];
                return port;
            }
        }

        public string OnlineAddress
        {
            get
            {
                var address = $"net.tcp://{ServerName}:{Port}";
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
