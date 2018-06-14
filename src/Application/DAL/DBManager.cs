using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DALContracts;
using System.Data.Common;
using CalculateEmails.Configuration.Contract;
using CalculateEmails.Autofac;
using Autofac;

namespace DAL
{
    public class DBManager : IDBManager
    {

        private string connectionString
        {
            get
            {
                IConfigurationClient client = AutofacContainer.Container.Resolve<IConfigurationClient>();
                //   IConfigurationClient client = IoCManager.IoCManager.GetSinglenstance<IConfigurationClient>();
                return client.GetSqlServerConnectionString();
            }
        }

        private string connectionStringServer
        {
            get
            {
                IConfigurationClient client = AutofacContainer.Container.Resolve<IConfigurationClient>();
                return client.GetDataSourceConnectionString();
            }
        }

        private void Read(SqlConnection connection, SqlTransaction transaction, CalculationDayDB result, DateTime date)
        {
            SqlCommand command = new SqlCommand("[outlook].[GetLastCalculationDay]");
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("Date", date));


            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.Read())
            {
                result.CalculateEmailsId = (int)sqlDataReader["CalculateEmailsId"];
                result.Date = (DateTime)sqlDataReader["Date"];
                result.MailCountAdd = (int)sqlDataReader["MailCountAdd"];
                result.MailCountSent = (int)sqlDataReader["MailCountSent"];
                result.MailCountProcessed = (int)sqlDataReader["MailCountProcessed"];
                result.TaskCountAdded = (int)sqlDataReader["TaskCountAdded"];
                result.TaskCountRemoved = (int)sqlDataReader["TaskCountRemoved"];
                result.TaskCountFinished = (int)sqlDataReader["TaskCountFinished"];
                sqlDataReader.Close();
                connection.Close();
            }
            else
            {
                throw new Exception("No record retrieved");
            }
        }

        private void Write(SqlConnection connection, SqlTransaction transaction, CalculationDayDB calcualtionDay)
        {
            SqlCommand command = new SqlCommand("[outlook].[UpdateLastCalculationDay]");
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@calculateEmailsId", calcualtionDay.CalculateEmailsId));
            command.Parameters.Add(new SqlParameter("@MailCountAdd", calcualtionDay.MailCountAdd));
            command.Parameters.Add(new SqlParameter("@MailCountSent", calcualtionDay.MailCountSent));
            command.Parameters.Add(new SqlParameter("@MailCountProcessed", calcualtionDay.MailCountProcessed));
            command.Parameters.Add(new SqlParameter("@TaskCountAdded", calcualtionDay.TaskCountAdded));
            command.Parameters.Add(new SqlParameter("@TaskCountRemoved", calcualtionDay.TaskCountRemoved));
            command.Parameters.Add(new SqlParameter("@TaskCountFinished", calcualtionDay.TaskCountFinished));
            var recordAffected = command.ExecuteNonQuery();
            if (recordAffected != 1)
            {
                throw new Exception("No record updated");
            }
        }

        public void UpdateCalculationDay(Action<CalculationDayDB> updateAction, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    CalculationDayDB result = new CalculationDayDB();
                    Read(connection, transaction, result, date);
                    updateAction(result);
                    Write(connection, transaction, result);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Close();
                }


            }
        }

        public CalculationDayDB GetLastCalculationDay(DateTime date)
        {
            CalculationDayDB result = new CalculationDayDB();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("[outlook].[GetLastCalculationDay]");
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("Date", date));
                connection.Open();
                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    result.CalculateEmailsId = (int)sqlDataReader["CalculateEmailsId"];
                    result.Date = (DateTime)sqlDataReader["Date"];
                    result.MailCountAdd = (int)sqlDataReader["MailCountAdd"];
                    result.MailCountSent = (int)sqlDataReader["MailCountSent"];
                    result.MailCountProcessed = (int)sqlDataReader["MailCountProcessed"];
                    result.TaskCountAdded = (int)sqlDataReader["TaskCountAdded"];
                    result.TaskCountRemoved = (int)sqlDataReader["TaskCountRemoved"];
                    result.TaskCountFinished = (int)sqlDataReader["TaskCountFinished"];
                }
                else
                {
                    throw new Exception("No record retrieved");
                }
                connection.Close();
            }

            return result;
        }

        public void PerformDatabaseupdate()
        {
            DBScripts.Scripts s = new DBScripts.Scripts();
            s.DatabaseUpdatePerform();
        }

        public void SaveTodayCalculationDay(CalculationDayDB calcualtionDay)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("[outlook].[UpdateLastCalculationDay]");
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@calculateEmailsId", calcualtionDay.CalculateEmailsId));
                command.Parameters.Add(new SqlParameter("@MailCountAdd", calcualtionDay.MailCountAdd));
                command.Parameters.Add(new SqlParameter("@MailCountSent", calcualtionDay.MailCountSent));
                command.Parameters.Add(new SqlParameter("@MailCountProcessed", calcualtionDay.MailCountProcessed));
                command.Parameters.Add(new SqlParameter("@TaskCountAdded", calcualtionDay.TaskCountAdded));
                command.Parameters.Add(new SqlParameter("@TaskCountRemoved", calcualtionDay.TaskCountRemoved));
                command.Parameters.Add(new SqlParameter("@TaskCountFinished", calcualtionDay.TaskCountFinished));
                connection.Open();
                var recordAffected = command.ExecuteNonQuery();
                if (recordAffected != 1)
                {
                    throw new Exception("No record updated");
                }
                connection.Close();

            }
        }

        //todo:it cannot be here
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

        //todo:it cannot be here
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


            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{

            //    SqlCommand command = new SqlCommand(@"EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'EcoVadisPTTest'");
            //    command.Connection = connection;
            //    command.CommandType = System.Data.CommandType.Text;
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //}

            //using (SqlConnection connection = new SqlConnection(connectionStringServer))
            //{

            //    SqlCommand command = new SqlCommand(@"DROP DATABASE[EcoVadisPTTest]");

            //    command.Connection = connection;
            //    connection.exe
            //    command.CommandType = System.Data.CommandType.Text;
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //}
        }
    }
}
