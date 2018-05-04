﻿using ConfigurationServiceClient;
using DBUpPT;
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
            string serverName = client.GetSqlDataSource();
            string dbName = client.GetSqlServerDataBaseName();

            DBUp dBUp = new DBUp("outlook");
            Assembly assembly = Assembly.GetExecutingAssembly();
            dBUp.PerformUpdate(serverName, dbName, assembly, false);

            //DatabaseUpdate db = new DatabaseUpdate("outlook");
            //db.PerformUpdate(client.GetSqlDataSource(),client.GetSqlServerDataBaseName(), Assembly.GetExecutingAssembly());
        }
    }
}