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

namespace CalculateEmails
{
    public partial class ThisAddIn
    {
        const string MainInboxName = "Inbox";
        const string CalculateEmails = "CalculateEmails";

        //BLManager emailManager;

        Outlook.Items inboxItems, sentItems, todoItems;
        Outlook.Explorer currentExplorer = null;
        private List<Outlook.Folder> InboxFolders { get; set; }// maybe delete?
        private List<Outlook.Items> listOfItems { get; set; }//maybe delete?

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.InboxFolders = new List<Outlook.Folder>();
            this.listOfItems = new List<Outlook.Items>();

            RegisterMainInboxHandlers();
            FindAllInboxFolders();
            RegisterSubInboxHandlers();

            SetLabelStartValue();

            ///ServiceClient client = new ServiceClient();
            ///client.GetData();

            ///this.emailManager = new BLManager();

            
            //FillDetailList();

            //UpdateLabel(emailManager.TodayCalculationDetails);
            Outlook.MAPIFolder inbox = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

            //inboxItems = inbox.Items;
            //inboxItems.ItemAdd += InboxFolderItemAdded;
            //  inboxItems.ItemRemove += InboxItems_ItemRemove;
            // inboxItems.ItemChange += InboxItems_ItemChange;

            RegisterSentItemsHanlder();
            TodoManage();

            currentExplorer = Globals.ThisAddIn.Application.ActiveExplorer();
            currentExplorer.SelectionChange += CurrentExplorer_SelectionChange;

        }

        private void SetLabelStartValue()
        {
           // throw new NotImplementedException();
        }

        private void RegisterMainInboxHandlers()
        {
            Outlook.Items mainInbox = Application.Session.DefaultStore.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Items;
            mainInbox.ItemRemove += MainInboxRemoved;
            mainInbox.ItemAdd += MainInboxAdded;
            mainInbox.ItemChange += MainInboxChanged;

            listOfItems.Add(mainInbox);
        }

        private void RegisterSubInboxHandlers()
        {
            foreach (Outlook.Folder folder in this.InboxFolders)
            {
                Outlook.Items folderItems = folder.Items;
                if (folder.Name!= MainInboxName)
                {
                    folderItems.ItemRemove += SubInboxRemoved;
                    folderItems.ItemAdd += SubInboxAdded;
                    folderItems.ItemChange += SubInboxChanged;
                }
               

                listOfItems.Add(folderItems);
            }
        }

        private void SubInboxChanged(object Item)
        {
            //throw new NotImplementedException();
        }

        private void FindAllInboxFolders()
        {
            FindAllInboxFoldersRecursive(Application.Session.DefaultStore.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Folders);
        }

        private void MainInboxAdded(object Item)
        {
            UpdateLabel(new ServiceClient().ProcessOutlookMail(InboxType.Main, EmailActionType.Added));
        }

        private void SubInboxAdded(object Item)
        {
            UpdateLabel(new ServiceClient().ProcessOutlookMail(InboxType.Subinbox, EmailActionType.Added));
        }

        private void MainInboxRemoved()
        {
            UpdateLabel(new ServiceClient().ProcessOutlookMail(InboxType.Main, EmailActionType.Removed));
        }

        private void SubInboxRemoved()
        {
            UpdateLabel(new ServiceClient().ProcessOutlookMail(InboxType.Subinbox, EmailActionType.Removed));
        }

        

        private void RegisterSentItemsHanlder()
        {
            Outlook.MAPIFolder sent = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
            sentItems = sent.Items;
            sentItems.ItemAdd += SentItems_ItemAdd;
        }

        private void SentItems_ItemAdd(object Item)
        {
            UpdateLabel(new ServiceClient().ProcessOutlookMail(InboxType.Sent, EmailActionType.Added));
        }

        private void MainInboxChanged(object Item)
        {
            // PerformChange(() => this.DetailsItems.TaskCountAdded++);
            //var i = Item as Outlook.MailItem;
            //if(i!=null)
            //{
            //    string s = (i.Parent as Outlook.Folder).Name;
            //}
        }


        private void FindAllInboxFoldersRecursive(Outlook.Folders folders)
        {
            foreach (Outlook.Folder folder in folders)
            {
                if (folder.Folders.Count > 0)
                {
                    FindAllInboxFoldersRecursive(folder.Folders);
                }
                if (folder.Name.StartsWith("Inbox"))
                {
                    this.InboxFolders.Add(folder);
                }
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


        private Outlook.NoteItem CheckCounterContainer()
        {
            Outlook.Explorer objExplorer = Globals.ThisAddIn.Application.ActiveExplorer();
            var folder = this.Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderNotes);
            var xx = folder.Items;
            foreach (Outlook.NoteItem item in xx)
            {
                if (item.Subject == CalculateEmails)
                {
                    return item;
                }
            }
            Outlook.NoteItem newContact = (Outlook.NoteItem)this.Application.CreateItem(Outlook.OlItemType.olNoteItem);
            try
            {
                newContact.Body = CalculateEmails + Environment.NewLine;
                newContact.Save();
            }
            catch
            {
                MessageBox.Show("The new contact was not saved.");
            }

            return newContact;
        }

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
