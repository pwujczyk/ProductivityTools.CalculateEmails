using ProductivityTools.CalculateEmails.WindowsService;
using ProductivityTools.MasterConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                MConfiguration.SetConfigurationFileName(args[0]);
            }
            else
            {
                MConfiguration.SetConfigurationFileName("Configuration.config");
            }
            PSCalculateEmails service = new PSCalculateEmails();
            service.OnDebug();
            Console.WriteLine("Host started");
            Console.Read();
        }
    }
}
