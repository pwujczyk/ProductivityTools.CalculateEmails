using ProductivityTools.CalculateEmails.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.BLTests
{
    public class Configuration : IConfig
    {
        public string MQAdress
        {
            get
            {
                var address = $"net.msmq://localhost/private/CalculateEmailsTest";
                return address;
            }
        }
        public string OnlineAddress => "net.tcp://localhost:6006";


        public string OnlineWebAddress => "http://localhost:6007";

        public string QueneName => "CalculateEmailsTest";

        public string GetSqlServerDataBaseName => "CalculateEmailsTest123"; 

        public string GetSqlDataSource => @".\sql2017";

        public string GetSqlServerConnectionString()
        {
            return ConnectionStringLightPT.ConnectionStringLight.GetSqlServerConnectionString(GetSqlDataSource, GetSqlServerDataBaseName);
        }

        public string GetDataSourceConnectionString()
        {
            return ConnectionStringLightPT.ConnectionStringLight.GetSqlDataSourceConnectionString(GetSqlDataSource);
        }
    }
}
