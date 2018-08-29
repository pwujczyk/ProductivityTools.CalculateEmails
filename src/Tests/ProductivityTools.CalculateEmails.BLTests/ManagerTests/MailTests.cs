using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.Service.Application;

namespace ProductivityTools.BLTests
{
    [TestClass]
    public class MailTests: BaseTestClass
    {
        [TestMethod]
        public void MailReferenceMethod()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void MailMailOneNewMail()
        {
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Added, InboxType.Main, Now);
            var x = bLManager.GetLastCalculationDay(Now);
            Assert.AreEqual(1, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }


        [TestMethod]
        public void SentOneMail()
        {
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Added, InboxType.Sent, Now);
            var x = bLManager.GetLastCalculationDay( Now);
            Assert.AreEqual(x.MailCountAdd, 0, MailCountAdd);
            Assert.AreEqual(x.MailCountProcessed, 0, MailCountProcessed);
            Assert.AreEqual(x.MailCountSent, 1);
            Assert.AreEqual(x.TaskCountAdded, 0);
            Assert.AreEqual(x.TaskCountFinished, 0);
            Assert.AreEqual(x.TaskCountRemoved, 0);
        }

        [TestMethod]
        public void MailMoveMailBetweenInboxesReverseOrder()
        {
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            var x = bLManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }

        [TestMethod]
        public void Mail()
        {
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            var x = bLManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }

        [TestMethod]
        public void MailMove4MailBetweenInboxesRightOrder()
        {
            BaseManager.WriteToLog("Move 4 mails between inboxes");
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            var x = bLManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(0, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved, TaskCountRemoved);
        }

        [TestMethod]
        public void MailMove6MailBetweenInboxesRightOrder()
        {
            BaseManager.WriteToLog("Move 4 mails between inboxes");
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Added, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            var x = bLManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(0, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved, TaskCountRemoved);
        }

        [TestMethod]
        public void MailProcessOneMailFromMainInbox()
        {
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Main, Now);
            var x = bLManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(1, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent);
            Assert.AreEqual(0, x.TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved);
        }


        [TestMethod]
        public void MailProcessTwoMailFromSubInbox()
        {
            BaseManager.WriteToLog("ProcessTwoMailFromSubInbox");
            MailManager bLManager = new MailManager();
            bLManager.Process(EmailActionType.Removed, InboxType.Subinbox, Now);
            bLManager.Process(EmailActionType.Removed, InboxType.Subinbox, Now);
            var x = bLManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(2, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(0, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved, TaskCountRemoved);
        }

    }
}
