using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.CalculateEmails.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.BLTests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Log()
        {
            //todo:change
            CalculateEmailsService c = new CalculateEmailsService();
            c.ProcessMail(CalculateEmails.Contract.DataContract.InboxType.Main, CalculateEmails.Contract.DataContract.EmailActionType.Added,DateTime.Now);
        }
    }
}
