using CalculateEmails.Configuration.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests
{
    public class Configuration : IConfigurationClient
    {
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
            return @"EcoVadisPTTest";
        }
    }
}
