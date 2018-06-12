using System;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.WCFService.Application;
using DALContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests
{
    [TestClass]
    public class UnitTest1
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            IDBManager DBManager = IoCManager.IoCManager.GetSinglenstance<IDBManager>();
            DBManager.PerformDatabaseupdate();
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
            IDBManager DBManager = IoCManager.IoCManager.GetSinglenstance<IDBManager>();
            DBManager.TruncateTable();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            IDBManager DBManager = IoCManager.IoCManager.GetSinglenstance<IDBManager>();
            DBManager.DropDatabase();
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
        }


        [TestMethod]
        public void ReferenceMethod()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void OneNewMail()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Added, InboxType.Main);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(1, x.MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }


        [TestMethod]
        public void SentOneMail()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Added, InboxType.Sent);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(x.MailCountAdd, 0);
            Assert.AreEqual(x.MailCountProcessed, 0);
            Assert.AreEqual(x.MailCountSent, 1);
            Assert.AreEqual(x.TaskCountAdded, 0);
            Assert.AreEqual(x.TaskCountFinished, 0);
            Assert.AreEqual(x.TaskCountRemoved, 0);
        }

        [TestMethod]
        public void MoveMailBetweenInboxesReverseOrder()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }

        [TestMethod]
        public void MoveMailBetweenInboxesRightOrder()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }

        [TestMethod]
        public void ProcessOneMailFromMainInbox()
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd);
            Assert.AreEqual(1, x.MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }

        [TestMethod]
        public void ProcessTwoMailFromSubInbox()
        {
            BaseManager.WriteToLog("ProcessTwoMailFromSubInbox");
            BLManager bLManager = new BLManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Subinbox);
            bLManager.Process(EmailActionType.Removed, InboxType.Subinbox);
            var x = bLManager.GetLastCalculationDay();
            Assert.AreEqual(0, x.MailCountAdd,"MailCountAdd");
            Assert.AreEqual(2, x.MailCountProcessed,"MailCountProcessed");
            Assert.AreEqual(0, x.MailCountSent,"Sent");
            Assert.AreEqual(0, x.TaskCountAdded,"TaskCountAdded");
            Assert.AreEqual(0, x.TaskCountFinished,"TaskCountFinished");
            Assert.AreEqual(0, x.TaskCountRemoved,"TaskCountRemoved");
        }
    }
}
