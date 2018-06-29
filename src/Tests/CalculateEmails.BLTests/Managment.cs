using System;
using Autofac;
using CalculateEmails.Autofac;
using CalculateEmails.Configuration;
using CalculateEmails.WCFService;
using DALContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests
{
    [TestClass]
    public class Managment
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {

            var builder = new ContainerBuilder();
            builder.RegisterModule<CalculateEmails.WCFService.Autofac>();
            builder.RegisterModule<CalculateEmails.ServiceClient.AutofacModule>();
            builder.RegisterType<Configuration>().As<IConfig>();

            AutofacContainer.Container = builder.Build();

            IDBManager DBManager = AutofacContainer.Container.Resolve<IDBManager>();
            DBManager.PerformDatabaseupdate();
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            new DBSetup().DropDatabase();
        }

    }
}
