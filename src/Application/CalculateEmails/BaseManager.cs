using DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace CalculateEmails
{
    class BaseManager
    {
        private IDBManager DBManager { get; set; }
        public CalculationDay TodayCalculationDetails { get; set; }

        public BaseManager()
        {
            DBManager=IoCManager.IoCManager.GetSinglenstance<IDBManager>();
            DBManager.PerformDatabaseupdate();
            FillTodaysCalculationDetails();
        }

        protected void PerformChange(Action a)
        {
            FillTodaysCalculationDetails();
            a();
            SaveDetailList();
           // UpdateLabel();
        }

        private void FillTodaysCalculationDetails()
        {
            //pw:to change
            this.TodayCalculationDetails = DBManager.GetLastCalculationDay(DateTime.Now);
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
        private void SaveDetailList()
        {
            DBManager.SaveTodayCalculationDay(TodayCalculationDetails);
        }

    }
}
