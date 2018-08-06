using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.DALContracts
{
    public interface IDBManager
    {
        //void UpdateCalculationDay(Action<CalculationDayDB> updateAction, DateTime date);
        List<CalculationDayDB> GetCalculationDays(DateTime startDate, DateTime endDay);
        void SaveTodayCalculationDay(CalculationDayDB calcualtionDay);
        void PerformDatabaseupdate();
    }
}
