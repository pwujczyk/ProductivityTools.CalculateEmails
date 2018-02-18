using ConfigurationServiceClient;
using DBUpPW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBScripts
{
    public class Scripts
    {
        public void DatabaseUpdatePerform()
        {
            ConfigurationClient client = new ConfigurationClient();            
            DatabaseUpdate db = new DatabaseUpdate("outlook");
            db.PerformUpdate(client.GetSqlDataSource(),client.GetSqlServerDataBaseName(), Assembly.GetExecutingAssembly());
        }
    }
}
