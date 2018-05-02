using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBScripts
{
    public class Scripts
    {
        public const string server = @".\sql2014";
        public const string dbName = "mBoxTest1";

        public void DatabaseUpdatePerform()
        {
          
            
            DBUpHelper.DBUp dBUp = new DBUpHelper.DBUp("outlook");
            dBUp.PerformUpdate(server,dbName,Assembly.GetExecutingAssembly(),true);
        }
    }
}
