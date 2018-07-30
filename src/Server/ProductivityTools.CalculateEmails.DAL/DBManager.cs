using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using Autofac;
using ProductivityTools.CalculateEmails.DALContracts;
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.Autofac;

namespace ProductivityTools.DAL
{
    public class DBManager : IDBManager
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

       
    }
}
