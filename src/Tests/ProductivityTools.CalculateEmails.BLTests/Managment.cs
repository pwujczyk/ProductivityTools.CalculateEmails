using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.DALContracts;
using ProductivityTools.DBScripts;

namespace ProductivityTools.BLTests
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
