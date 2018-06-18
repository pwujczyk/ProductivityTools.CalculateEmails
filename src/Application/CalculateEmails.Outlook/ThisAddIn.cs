using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CalculateEmails.Contract.DataContract;
using System.Threading;
using System.Diagnostics;
using CalculateEmails.ServiceClient;
using Autofac;
using CalculateEmails.Autofac;

namespace CalculateEmails
{
    public partial class ThisAddIn
    {
        const string MainInboxName = "Inbox";
        const string CalculateEmails = "CalculateEmails";

        public static bool ServiceIsWorking;


        //BLManager emailManager;

        Outlook.Items inboxItems, sentItems, todoItems;
        Outlook.Explorer currentExplorer = null;
        private List<Outlook.Folder> InboxFolders { get; set; }// maybe delete?
        private List<Outlook.Items> listOfItems { get; set; }//maybe delete?

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            AutofacContainer.Container = builder.Build();

            this.CalculateEmailsEnabled = true;
            MasterConfiguration.MConfiguration.SetCurrentDomainPath(true);
            Globals.Ribbons.CalculateEmails.btnClearInvitation.Click += BtnClearInvitation_Click;

            Thread t = new Thread(new ThreadStart(HeartBeatChecker));
            //  t.Start();

            this.InboxFolders = new List<Outlook.Folder>();
            this.listOfItems = new List<Outlook.Items>();

            MailsManage();
            TodoManage();

            // Outlook.MAPIFolder inbox = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

            //inboxItems = inbox.Items;
            //inboxItems.ItemAdd += InboxFolderItemAdded;
            //  inboxItems.ItemRemove += InboxItems_ItemRemove;
            // inboxItems.ItemChange += InboxItems_ItemChange;




            currentExplorer = Globals.ThisAddIn.Application.ActiveExplorer();
            currentExplorer.SelectionChange += CurrentExplorer_SelectionChange;

        }

        private void BtnClearInvitation_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            CalculateEmailsEnabled = false;
            Outlook.MAPIFolder inbox = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            foreach (var item in inbox.Items)
            {
                Outlook.MailItem mail = item as Outlook.MailItem;
                if (mail != null && mail.Subject != null)
                {
                    Debug.WriteLine(mail.Subject);
                    if (mail.Subject.StartsWith("Accepted: CAP estimation"))
                    {
                        mail.Delete();
                        Console.Write("fdsa");
                    }
                }

            }
            CalculateEmailsEnabled = true;
        }

        private void WriteToLog(string message)
        {
            string sSource;
            string sLog;
            string sEvent;

            string applicationName = "CalculateEmails";
            sSource = "HeartBeat";
            sLog = "Application";
            sEvent = "Sample Event";



            if (!EventLog.SourceExists(applicationName))
            {
                EventLog.CreateEventSource(sSource, applicationName);
            }

            EventLog.WriteEntry(sSource, sEvent);
            EventLog.WriteEntry(sSource, sEvent,
            EventLogEntryType.Warning, 234);


            string source = "DemoTestApplication";
            string log = "DemoEventLog";
            EventLog demoLog = new EventLog(log);
            demoLog.Source = source;
            demoLog.WriteEntry("This is the first message to the log", EventLogEntryType.Information);
        }

        public void HeartBeatChecker()
        {
            bool result = true;
            while (true)
            {
                try
                {
                    new WcfClient().HeartBeat();
                    ServiceIsWorking = true;
                    WriteToLog("Outlook calculate emails service working correctly. HeartBeat OK.");
                }
                catch (Exception ex)
                {
                    ServiceIsWorking = false;
                    WriteToLog("Outlook calculate emails service not working. Dead.");
                }

                Globals.Ribbons.CalculateEmails.chHeartBeat.Checked = ServiceIsWorking;
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }

        private void CurrentExplorer_SelectionChange()
        {
            if (currentExplorer.Selection.Count > 0)
            {
                var x = currentExplorer.Selection[1];
                Outlook.MailItem mail = x as Outlook.MailItem;
                if (mail != null)
                {
                    var i = mail.FlagStatus;
                }
            }
        }

        //private void InboxItems_ItemChange(object Item)
        //{
        //    Outlook.MailItem mail = Item as Outlook.MailItem;
        //    if (mail!=null) 
        //    {
        //        var i = mail.FlagStatus;
        //    }
        //}



        //private void InboxItems_ItemRemove()
        //{
        //    ProcessedInboxItem();
        //}


        //private Outlook.NoteItem CheckCounterContainer()
        //{
        //    Outlook.Explorer objExplorer = Globals.ThisAddIn.Application.ActiveExplorer();
        //    var folder = this.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderNotes);
        //    var xx = folder.Items;
        //    foreach (Outlook.NoteItem item in xx)
        //    {
        //        if (item.Subject == CalculateEmails)
        //        {
        //            return item;
        //        }
        //    }
        //    Outlook.NoteItem newContact = (Outlook.NoteItem)this.Application.CreateItem(Outlook.OlItemType.olNoteItem);
        //    try
        //    {
        //        newContact.Body = CalculateEmails + Environment.NewLine;
        //        newContact.Save();
        //    }
        //    catch
        //    {
        //        MessageBox.Show("The new contact was not saved.");
        //    }

        //    return newContact;
        //}

        private DateTime CurrentDay
        {
            get
            {
                return DateTime.Today;
            }
        }

        private void UpdateLabel()
        {
            //UpdateLabel(emailManager.TodayCalculationDetails);
        }


        private void UpdateLabel(CalculationDay calculationDay)
        {
            Globals.Ribbons.CalculateEmails.lblInCounter.Label = calculationDay.MailCountAdd.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblOutCouter.Label = calculationDay.MailCountSent.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblProcessedCounter.Label = calculationDay.MailCountProcessed.ToString(); ;

            Globals.Ribbons.CalculateEmails.lblTaskAdd.Label = calculationDay.TaskCountAdded.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblTaskRemoved.Label = calculationDay.TaskCountRemoved.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblTaskFinished.Label = calculationDay.TaskCountFinished.ToString(); ;

            Globals.Ribbons.CalculateEmails.lblInCounter2.Label = calculationDay.MailCountAdd.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblOutCouter2.Label = calculationDay.MailCountSent.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblProcessedCounter2.Label = calculationDay.MailCountProcessed.ToString(); ;

            Globals.Ribbons.CalculateEmails.lblTaskAdd2.Label = calculationDay.TaskCountAdded.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblTaskRemoved2.Label = calculationDay.TaskCountRemoved.ToString(); ;
            Globals.Ribbons.CalculateEmails.lblTaskFinished2.Label = calculationDay.TaskCountFinished.ToString(); ;
        }


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
