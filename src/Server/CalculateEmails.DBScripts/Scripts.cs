using Autofac;
using CalculateEmails.Autofac;
using CalculateEmails.Configuration;
using DBUpPT;
using System.Reflection;

namespace DBScripts
{
    public class Scripts
    {
        public void DatabaseUpdatePerform()
        {
            //IConfigurationClient client = IoCManager.IoCManager.GetSinglenstance<IConfigurationClient>();;
            IConfig client = AutofacContainer.Container.Resolve<IConfig>();
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
