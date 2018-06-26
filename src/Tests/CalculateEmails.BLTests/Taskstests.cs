using System;
using CalculateEmails.WCFService.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests
{
    [TestClass]
    public class TasksTests : BaseTestClass
    {
        [TestMethod]
        public void TaskReferenceMethod()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void RemoveOneTask()
        {
            TaskManager taskManager = new TaskManager();
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Removed, Now);
            var x = taskManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(0, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(1, x.TaskCountRemoved, TaskCountRemoved);
        }


        [TestMethod]
        public void AddOneTask()
        {
            TaskManager taskManager = new TaskManager();
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Added, Now);
            var x = taskManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(1, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(0, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved, TaskCountRemoved);
        }

        [TestMethod]
        public void FinishOneTask()
        {
            TaskManager taskManager = new TaskManager();
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Finished, Now);
            var x = taskManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(0, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(1, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(0, x.TaskCountRemoved, TaskCountRemoved);
        }

        [TestMethod]
        public void Add2Finish3Remove4Task()
        {
            TaskManager taskManager = new TaskManager();
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Added, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Removed, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Removed, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Finished, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Removed, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Removed, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Finished, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Finished, Now);
            taskManager.Process(CalculateEmails.Contract.DataContract.TaskActionType.Added, Now);
            var x = taskManager.GetLastCalculationDay(Now);
            Assert.AreEqual(0, x.MailCountAdd, MailCountAdd);
            Assert.AreEqual(0, x.MailCountProcessed, MailCountProcessed);
            Assert.AreEqual(0, x.MailCountSent, Sent);
            Assert.AreEqual(2, x.TaskCountAdded, TaskCountAdded);
            Assert.AreEqual(3, x.TaskCountFinished, TaskCountFinished);
            Assert.AreEqual(4, x.TaskCountRemoved, TaskCountRemoved);
        }


    }
}
