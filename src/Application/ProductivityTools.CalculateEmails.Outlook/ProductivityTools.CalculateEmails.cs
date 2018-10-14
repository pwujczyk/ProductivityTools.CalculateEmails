using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Autofac;
using ProductivityTools.CalculateEmails.ServiceClient;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.Autofac;

namespace ProductivityTools.CalculateEmails
{
    public partial class ThisAddIn
    {
        const string MainInboxName = "Inbox";
        const string CalculateEmails = "CalculateEmails";

        public static bool ServiceIsWorking = false;
        private int InvitationsCounter;


        //BLManager emailManager;

        Outlook.Items inboxItems, sentItems, todoItems;
        Outlook.Explorer currentExplorer = null;
        private List<Outlook.Folder> InboxFolders { get; set; }// maybe delete?
        private List<Outlook.Items> listOfItems { get; set; }//maybe delete?

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            ProductivityTools.MasterConfiguration.MConfiguration.SetCurrentDomainPath(true);
            ProductivityTools.MasterConfiguration.MConfiguration.SetConfigurationFileName("Configuration.config");
            RegisterAutofac();

            this.CalculateEmailsEnabled = true;

            Globals.Ribbons.CalculateEmails.btnClearInvitation.Click += BtnClearInvitation_Click;

            Thread t = new Thread(new ThreadStart(HeartBeatChecker));
            t.Start();
            DelayAndUpdateLabelWrapper();

            this.InboxFolders = new List<Outlook.Folder>();
            this.listOfItems = new List<Outlook.Items>();

            MailsManage();
            TodoManage();

            currentExplorer = Globals.ThisAddIn.Application.ActiveExplorer();
            currentExplorer.SelectionChange += CurrentExplorer_SelectionChange;

        }

        private static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            AutofacContainer.Container = builder.Build();
        }

        private void BtnClearInvitation_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            Func<string, bool> f = s => s.StartsWith("Accepted: ") || s.StartsWith("Declined: ") || s.StartsWith("Tentatively Accepted: ");

            InvitationsCounter = 0;

            Outlook.MAPIFolder inbox = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            for (int i = inbox.Items.Count; i > 0; i--)
            //foreach (var item in inbox.Items)
            {
                var item = inbox.Items[i];
                Outlook.MailItem mail = item as Outlook.MailItem;
                if (mail != null && mail.Subject != null)
                {
                    //todo: move it to configuration
                    if (f(mail.Subject))
                    {
                        mail.Delete();
                        InvitationsCounter++;
                    }
                }
                Outlook.MeetingItem meeting = item as Outlook.MeetingItem;
                if (meeting != null)
                {
                    if (f(meeting.ConversationTopic))
                    {
                        meeting.Delete();
                        InvitationsCounter++;
                    }
                }
            }
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
            while (true)
            {
                try
                {
                    new StatsClient().HeartBeat();
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

        //private DateTime CurrentDay
        //{
        //    get
        //    {
        //        return DateTime.Today;
        //    }
        //}

        private void UpdateLabel()
        {
            //UpdateLabel(emailManager.TodayCalculationDetails);
        }


        private void DelayAndUpdateLabelWrapper()
        {
            if (this.CalculateEmailsEnabled)
            {
                Thread t = new Thread(new ThreadStart(DelayAndUpdateLabel));
                t.Start();
            }
        }

        private void DelayAndUpdateLabel()
        {
            Thread.Sleep(1000);
            UpdateCalculateDetails();
        }

        private void UpdateCalculateDetails()
        {
            var calculationDay = GetCalculationDayDetails();
            UpdateLabelControls(calculationDay);
        }

        private void UpdateLabelControls(CalculationDay calculationDay)
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

        private CalculationDay GetCalculationDayDetails()
        {
            StatsClient onlineClient = new StatsClient();
            CalculationDay calculationDay = onlineClient.GetCalculationDay();
            return calculationDay;
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
