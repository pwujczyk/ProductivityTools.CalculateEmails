using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DALContracts;
using DAL;

namespace DALTests
{
    [TestClass]
    public class DALEmailTests
    {
        IDBManager manager;

        private DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            manager = new DBManager();
        }

        [TestMethod]
        public void GetCurrentCalculateDay()
        {
            var day=manager.GetLastCalculationDay(Now);
            Assert.IsNotNull(day);
            Assert.AreEqual(day.Date, Now.Date);
        }

        [TestMethod]
        public void SaveCalcuateDay()
        {
            int testedValue = 987;
            CalculationDayDB c =  manager.GetLastCalculationDay(Now);
            c.MailCountAdd = testedValue;
            manager.SaveTodayCalculationDay(c);

            var day = manager.GetLastCalculationDay(Now);
            Assert.AreEqual(day.MailCountAdd, testedValue);
        }
    }
}
