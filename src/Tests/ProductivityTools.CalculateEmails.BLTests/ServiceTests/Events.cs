using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.ServiceClient;
using ProductivityTools.CalculateEmails.WindowsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.BLTests.ServiceTests
{
    [TestClass]
    public class Events
    {
        [TestMethod]
        [TestCategory("Service")]
        public void NewEmail()
        {
            //MConfiguration.SetConfigurationName("Configuration.config");
            var builder = new ContainerBuilder();
            builder.RegisterModule<CalculateEmails.Service.Autofac>();
            builder.RegisterModule<CalculateEmails.ServiceClient.AutofacModule>();
            builder.RegisterType<Configuration>().As<IConfig>();

            AutofacContainer.Container = builder.Build();

            PSCalculateEmails calculateEmails = new PSCalculateEmails();
            calculateEmails.OnTest();

            ProcessingClient client = new ProcessingClient();
            client.ProcessOutlookMail(CalculateEmails.Contract.DataContract.InboxType.Main, CalculateEmails.Contract.DataContract.EmailActionType.Added);


            StatsClient onlineClient = new StatsClient();
            var stats=onlineClient.GetCalculationDay();
            Assert.AreEqual(1, stats.MailCountAdd);
        }
    }
}
