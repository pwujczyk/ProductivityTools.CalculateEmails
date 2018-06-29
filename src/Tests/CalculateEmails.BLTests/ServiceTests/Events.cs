using Autofac;
using CalculateEmails.Autofac;
using CalculateEmails.Configuration;
using CalculateEmails.ServiceClient;
using CalculateEmails.WindowsService;
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
        [TestCategory("Service")]
        public void NewEmail()
        {
            //MConfiguration.SetConfigurationName("Configuration.config");
            var builder = new ContainerBuilder();
            builder.RegisterModule<CalculateEmails.WCFService.Autofac>();
            builder.RegisterModule<CalculateEmails.ServiceClient.AutofacModule>();
            builder.RegisterType<Configuration>().As<IConfig>();

            AutofacContainer.Container = builder.Build();

            PSCalculateEmails calculateEmails = new PSCalculateEmails();
            calculateEmails.OnTest();

            WcfClient client = new WcfClient();
            client.ProcessOutlookMail(CalculateEmails.Contract.DataContract.InboxType.Main, CalculateEmails.Contract.DataContract.EmailActionType.Added);


            OnlineClient onlineClient = new OnlineClient();
            var stats=onlineClient.GetCalculationDay();
            Assert.AreEqual(1, stats.MailCountAdd);
        }
    }
}
