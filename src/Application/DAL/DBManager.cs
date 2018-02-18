using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DALContracts;
using ConfigurationServiceClient;

namespace DAL
{
    public class DBManager : IDBManager
    {

        private string connectionString
        {
            get
            {
                ConfigurationClient client = new ConfigurationClient();
                return client.GetSqlServerConnectionString();
            }
        }

        public CalculationDay GetLastCalculationDay(DateTime date)
        {
            CalculationDay result = new CalculationDay();
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
            }

            return result;
        }

        public void PerformDatabaseupdate()
        {
            DBScripts.Scripts s = new DBScripts.Scripts();
            s.DatabaseUpdatePerform();
        }

        public void SaveTodayCalculationDay(CalculationDay calcualtionDay)
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

            }
        }
    }
}
