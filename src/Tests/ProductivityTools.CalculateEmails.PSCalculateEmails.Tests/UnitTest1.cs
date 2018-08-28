using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.ServiceClient;
using ProductivityTools.DateTimeTools;

namespace ProductivityTools.CalculateEmails.PSCalculateEmails.Tests
{
    [TestClass]
    public class UnitTest1
    {
        List<CalculationDay> calcuationDayList = new List<CalculationDay>();

        [TestMethod]
        public void PrintLastStatsDefault()
        {
            DateTime requestedDateEnd = DateTime.MinValue, requestedDateStart = DateTime.MinValue;
            DateTime Now = DateTime.Parse("2018.06.20");

            Mock<IStatsClient> client = new Mock<IStatsClient>();
            client.Setup(x => x.GetStats(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Callback((DateTime a, DateTime b) => { requestedDateStart = a; requestedDateEnd = b; })
                .Returns(calcuationDayList);

            Mock<IDateTimePT> dt = new Mock<IDateTimePT>();
            dt.Setup(x => x.Now).Returns(Now);

            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            builder.RegisterInstance(client.Object).As<IStatsClient>();
            builder.RegisterInstance(dt.Object).As<IDateTimePT>();
            AutofacContainer.Container = builder.Build();


            PSCalculateEmails cmdlet = new PSCalculateEmails(true);
            MasterConfiguration.MConfiguration.SetConfigurationName("ConfigurationTest.config");
            cmdlet.Test();

            Assert.AreEqual(Now, requestedDateEnd);
            Assert.AreEqual(Now, requestedDateStart.AddDays(10));

            cmdlet.LastDays = 2;
            cmdlet.Test();
            Assert.AreEqual(Now, requestedDateEnd);
            Assert.AreEqual(Now.AddDays(-2), requestedDateStart);

            cmdlet.StartDate = Now.AddDays(-1);
            cmdlet.Test();
            Assert.AreEqual(Now.AddDays(-1), requestedDateStart);
            Assert.AreEqual(Now, requestedDateEnd);

            cmdlet.StartDate = Now.AddDays(-10);
            cmdlet.EndDate = Now.AddDays(-1);
            cmdlet.Test();
            Assert.AreEqual(Now.AddDays(-10), requestedDateStart);
            Assert.AreEqual(Now.AddDays(-1), requestedDateEnd);


        }



        //private void SetupEnvironment()
        //{
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-1), MailCountAdd = 1, MailCountProcessed = 1, MailCountSent = 1, TaskCountAdded = 1, TaskCountFinished = 1, TaskCountRemoved = 1 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-2), MailCountAdd = 2, MailCountProcessed = 2, MailCountSent = 2, TaskCountAdded = 2, TaskCountFinished = 2, TaskCountRemoved = 2 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-3), MailCountAdd = 3, MailCountProcessed = 3, MailCountSent = 3, TaskCountAdded = 3, TaskCountFinished = 3, TaskCountRemoved = 3 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-4), MailCountAdd = 4, MailCountProcessed = 4, MailCountSent = 4, TaskCountAdded = 4, TaskCountFinished = 4, TaskCountRemoved = 4 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-5), MailCountAdd = 5, MailCountProcessed = 5, MailCountSent = 5, TaskCountAdded = 5, TaskCountFinished = 5, TaskCountRemoved = 5 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-6), MailCountAdd = 6, MailCountProcessed = 6, MailCountSent = 6, TaskCountAdded = 6, TaskCountFinished = 6, TaskCountRemoved = 6 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-7), MailCountAdd = 7, MailCountProcessed = 7, MailCountSent = 7, TaskCountAdded = 7, TaskCountFinished = 7, TaskCountRemoved = 7 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-8), MailCountAdd = 8, MailCountProcessed = 8, MailCountSent = 8, TaskCountAdded = 8, TaskCountFinished = 8, TaskCountRemoved = 8 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-9), MailCountAdd = 9, MailCountProcessed = 9, MailCountSent = 9, TaskCountAdded = 9, TaskCountFinished = 9, TaskCountRemoved = 9 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-10), MailCountAdd = 10, MailCountProcessed = 10, MailCountSent = 10, TaskCountAdded = 10, TaskCountFinished = 10, TaskCountRemoved = 10 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-11), MailCountAdd = 11, MailCountProcessed = 11, MailCountSent = 11, TaskCountAdded = 11, TaskCountFinished = 11, TaskCountRemoved = 11 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-12), MailCountAdd = 12, MailCountProcessed = 12, MailCountSent = 12, TaskCountAdded = 12, TaskCountFinished = 12, TaskCountRemoved = 12 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-13), MailCountAdd = 13, MailCountProcessed = 13, MailCountSent = 13, TaskCountAdded = 13, TaskCountFinished = 13, TaskCountRemoved = 13 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-14), MailCountAdd = 14, MailCountProcessed = 14, MailCountSent = 14, TaskCountAdded = 14, TaskCountFinished = 14, TaskCountRemoved = 14 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-15), MailCountAdd = 15, MailCountProcessed = 15, MailCountSent = 15, TaskCountAdded = 15, TaskCountFinished = 15, TaskCountRemoved = 15 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-16), MailCountAdd = 16, MailCountProcessed = 16, MailCountSent = 16, TaskCountAdded = 16, TaskCountFinished = 16, TaskCountRemoved = 16 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-17), MailCountAdd = 17, MailCountProcessed = 17, MailCountSent = 17, TaskCountAdded = 17, TaskCountFinished = 17, TaskCountRemoved = 17 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-18), MailCountAdd = 18, MailCountProcessed = 18, MailCountSent = 18, TaskCountAdded = 18, TaskCountFinished = 18, TaskCountRemoved = 18 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-19), MailCountAdd = 19, MailCountProcessed = 19, MailCountSent = 19, TaskCountAdded = 19, TaskCountFinished = 19, TaskCountRemoved = 19 });
        //    calcuationDayList.Add(new CalculationDay() { Date = DateTime.Now.AddDays(-20), MailCountAdd = 20, MailCountProcessed = 20, MailCountSent = 20, TaskCountAdded = 20, TaskCountFinished = 20, TaskCountRemoved = 20 });


        //    Mock<IStatsClient> client = new Mock<IStatsClient>();
        //    client.Setup(x => x.GetStats(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
        //        .Returns(calcuationDayList);

        //    var builder = new ContainerBuilder();
        //    builder.RegisterModule<AutofacModule>();
        //    builder.RegisterInstance(client.Object).As<IStatsClient>();
        //    AutofacContainer.Container = builder.Build();
        //}
    }
}
