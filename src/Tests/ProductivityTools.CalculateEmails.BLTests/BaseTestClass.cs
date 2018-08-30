
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.BLTests
{
    public class BaseTestClass
    {
        protected string MailCountAdd = "MailCountAdd";
        protected string MailCountProcessed = "MailCountProcessed";
        protected string Sent = "MailSent";
        protected string TaskCountAdded = "TaskCountAdded";
        protected string TaskCountFinished = "TaskCountFinished";
        protected string TaskCountRemoved = "TaskCountRemoved";
        //todo: change
        protected DateTime Now
        {
            get { return DateTime.Now; }
        }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {

        }

        [TestInitialize()]
        public void Initialize()
        {
        }

        [TestCleanup()]
        public void Cleanup()
        {
            new DBSetup().TruncateTable();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {

        }


    }
}
