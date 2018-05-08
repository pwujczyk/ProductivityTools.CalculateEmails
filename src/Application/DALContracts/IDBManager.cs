using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALContracts
{
    public interface IDBManager
    {
        CalculationDayDB GetLastCalculationDay(DateTime date);
        void SaveTodayCalculationDay(CalculationDayDB calcualtionDay);
        void PerformDatabaseupdate();
    }
}
