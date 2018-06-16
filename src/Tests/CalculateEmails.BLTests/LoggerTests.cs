using CalculateEmails.WCFService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Log()
        {
            CalculateEmailsWCFService c = new CalculateEmailsWCFService();
            c.ProcessMail(CalculateEmails.Contract.DataContract.InboxType.Main, CalculateEmails.Contract.DataContract.EmailActionType.Added);
        }
    }
}
