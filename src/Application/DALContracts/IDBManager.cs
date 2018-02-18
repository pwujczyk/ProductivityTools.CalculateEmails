using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALContracts
{
    public interface IDBManager
    {
        CalculationDay GetLastCalculationDay(DateTime date);
        void SaveTodayCalculationDay(CalculationDay calcualtionDay);
        void PerformDatabaseupdate();
    }
}
