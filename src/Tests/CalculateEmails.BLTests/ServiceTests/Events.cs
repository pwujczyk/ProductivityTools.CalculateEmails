using CalculateEmails.ServiceClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.ServiceTests
{
    [TestClass]
    public class Events
    {
        [TestMethod]
        public void NewEmail()
        {
            WcfClient client = new WcfClient();
            client.ProcessOutlookMail(CalculateEmails.Contract.DataContract.InboxType.Main, CalculateEmails.Contract.DataContract.EmailActionType.Added);
        }
    }
}
