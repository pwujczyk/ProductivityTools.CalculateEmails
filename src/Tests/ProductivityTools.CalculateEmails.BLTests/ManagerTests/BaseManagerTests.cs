using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.BLTests;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.Service.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.ManagerTests
{
    [TestClass]
    public class BaseManagerTests : BaseTestClass
    {
        public void GetLastStats()
        {

            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Added, InboxType.Main, Now);
            var x = bLManager.GetLastCalculationDay(Now);

            BaseManager baseManager = new BaseManager();
            baseManager.GetCalcuationDays(DateTime.MinValue, DateTime.MaxValue);

        }
    }
}
