using AutoMapper;
using CalculateEmails.Contract.DataContract;
using DALContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.WCFService.Application
{
    class BaseManager
    {

        private DateTime Now { get { return DateTime.Now; } }
        private IDBManager DBManager { get; set; }
        public CalculationDayDB TodayCalculationDetails { get; set; }

        private MapperConfiguration mapperConfiguration;

        public BaseManager()
        {
            DBManager = IoCManager.IoCManager.GetSinglenstance<IDBManager>();
            DBManager.PerformDatabaseupdate();
            FillTodaysCalculationDetails();

            
            //or
        
        }

        protected void WriteToLog(string message)
        {
            Console.WriteLine(message);
            string sSource;
            //string sLog;
            //string sEvent;

            string applicationName = "CalculateEmails";
            sSource = "MailManager";
            //sLog = "Application";
            //sEvent = "Sample Event";

            message = message + " " + DateTime.Now.ToLongTimeString();

            if (!EventLog.SourceExists(applicationName))
            {
                EventLog.CreateEventSource(sSource, applicationName);
            }

            EventLog.WriteEntry(sSource, message);
            //EventLog.WriteEntry(sSource, message, EventLogEntryType.Warning, 234);


        }


        protected void PerformChange(Action a)
        {
            FillTodaysCalculationDetails();
            WriteToLog($"PerformChange Over A= TodayCalculationDetails.MailCountAdd: {TodayCalculationDetails.MailCountAdd}");
            a();
            WriteToLog($"PerformChange Under A= TodayCalculationDetails.MailCountAdd: {TodayCalculationDetails.MailCountAdd}");
            SaveDetailList();
            // UpdateLabel();
        }

        private void FillTodaysCalculationDetails()
        {
            //pw:to change
            lock (padloc)
            {
                this.TodayCalculationDetails = DBManager.GetLastCalculationDay(Now);
            }
        }

        //private void FillDetailList()
        //{
        //    Outlook.NoteItem noteItem = CheckCounterContainer();
        //    string body = noteItem.Body.Replace(CalculateEmails, "").Trim();
        //    Details = body.Deserialize<List<CalculationDetails>>();

        //    if (Details == null)
        //    {
        //        Details = new List<CalculationDetails>();
        //    }
        //    var currentDay = Details.FirstOrDefault(x => x.Date == CurrentDay);
        //    if (currentDay == null)
        //    {
        //        Details.Add(new CalculationDetails() { Date = DateTime.Today, MailCountAdd = 0 });
        //    }
        //}

        //private void SaveDetailList()
        //{
        //    Outlook.NoteItem noteItem = CheckCounterContainer();
        //    noteItem.Body = CalculateEmails + Environment.NewLine + Details.Serialize();
        //    noteItem.Save();
        //}

        private static readonly object padloc = new object();

        private void SaveDetailList()
        {
            lock (padloc)
            {
                WriteToLog($"TodayCalculationDetails.MailCountAdd: {TodayCalculationDetails.MailCountAdd}");
                DBManager.SaveTodayCalculationDay(TodayCalculationDetails);
            }
        }

    }
}
