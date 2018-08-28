using ProductivityTools.MasterConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Configuration
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
        private string PortNetTcp
        {
            get
            {
                var port =  MConfiguration.Configuration["PortNetTcp"];
                return port;
            }
        }

        private string PortWebApi
        {
            get
            {
                var port = MConfiguration.Configuration["PortWebApi"];
                return port;
            }
        }


        public string OnlineAddress
        {
            get
            {
                var address = $"net.tcp://{ServerName}:{PortNetTcp}";
                return address;
            }
        }

        public string OnlineWebAddress
        {
            get
            {
                var address = $"http://{ServerName}:{PortWebApi}";
                return address;
            }
        }

        public string GetSqlServerConnectionString()
        {
            return ConnectionStringLightPT.ConnectionStringLight.GetSqlServerConnectionString(GetSqlDataSource, GetSqlServerDataBaseName);
        }


        public string GetDataSourceConnectionString()
        {
            return ConnectionStringLightPT.ConnectionStringLight.GetSqlDataSourceConnectionString(GetSqlDataSource);
        }

        public string GetSqlDataSource
        {
            get
            {
                var serverName = MConfiguration.Configuration["DatabaseServerName"];
                return serverName;
            }
        }

        public string GetSqlServerDataBaseName
        {
            get
            {
                var databaseName = MConfiguration.Configuration["DatabaseName"];
                return databaseName;
            }
        }
    }
}
