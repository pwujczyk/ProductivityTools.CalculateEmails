using Autofac;
using CalculateEmails.Autofac;
using Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests
{
    class DBSetup
    {
        private string connectionString
        {
            get
            {
                IConfig client = AutofacContainer.Container.Resolve<IConfig>();
                //   IConfigurationClient client = IoCManager.IoCManager.GetSinglenstance<IConfigurationClient>();
                return client.GetSqlServerConnectionString();
            }
        }

        private string connectionStringServer
        {
            get
            {
                IConfig client = AutofacContainer.Container.Resolve<IConfig>();
                return client.GetDataSourceConnectionString();
            }
        }

        public void TruncateTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("TRUNCATE TABLE [outlook].[CalculateEmails]");
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DropDatabase()
        {
            using (SqlConnection con = new SqlConnection(connectionStringServer))
            {
                con.Open();
                String sqlCommandText = @"
                ALTER DATABASE EcoVadisPTTest SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [EcoVadisPTTest]";
                SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
