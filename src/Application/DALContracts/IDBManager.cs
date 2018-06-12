using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALContracts
{
    public interface IDBManager
    {
        //void UpdateCalculationDay(Action<CalculationDayDB> updateAction, DateTime date);
        CalculationDayDB GetLastCalculationDay(DateTime date);
        void SaveTodayCalculationDay(CalculationDayDB calcualtionDay);
        void PerformDatabaseupdate();

        void TruncateTable();
        void DropDatabase();
    }
}
