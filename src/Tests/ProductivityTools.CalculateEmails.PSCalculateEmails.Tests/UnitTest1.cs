using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductivityTools.CalculateEmails.PSCalculateEmails.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           // var service = new ProductivityTools.CalculateEmails.WindowsService.PSCalculateEmails();
            PSCalculateEmails cmdlet = new PSCalculateEmails();

            MasterConfiguration.MConfiguration.SetConfigurationName("ConfigurationTest.config");

          //  service.OnDebug();
            cmdlet.Test();
        }
    }
}
