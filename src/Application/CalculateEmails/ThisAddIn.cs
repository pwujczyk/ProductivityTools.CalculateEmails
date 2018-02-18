using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DALContracts;

namespace CalculateEmails
{
    public partial class ThisAddIn
    {
        const string CalculateEmails = "CalculateEmails";

        BLManager emailManager;

        Outlook.Items inboxItems, sentItems, todoItems;
        Outlook.Explorer currentExplorer = null;
        private List<Outlook.Folder> InboxFolders { get; set; }
        List<Outlook.Items> listOfItems { get; set; }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.emailManager = new BLManager();

            InboxFolders = new List<Microsoft.Office.Interop.Outlook.Folder>();
            this.listOfItems = new List<Outlook.Items>();
            //FillDetailList();
            UpdateLabel(emailManager.TodayCalculationDetails);
            Outlook.MAPIFolder inbox = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

            inboxItems = inbox.Items;
            //inboxItems.ItemAdd += InboxFolderItemAdded;
            //  inboxItems.ItemRemove += InboxItems_ItemRemove;
            // inboxItems.ItemChange += InboxItems_ItemChange;

            SentItems();
            TodoManage();

            currentExplorer = Globals.ThisAddIn.Application.ActiveExplorer();
            currentExplorer.SelectionChange += CurrentExplorer_SelectionChange;

            PerformCalculation2();
        }

        //private void PerformCalculation()
        //{
        //    FindAllInboxFolders(Application.Session.Folders);
        //    //FindAllInboxFolders(Application.Session.DefaultStore.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Folders);
        //    foreach (Outlook.Folder folder in this.InboxFolders)
        //    {
        //        Outlook.Items it = folder.Items;
        //        if (folder.Name == "Inbox")
        //        {
        //            it.ItemRemove += InboxItemRemovedProcessed;
        //        }
        //        else
        //        {
        //            it.ItemRemove += SubInboxRemoved;
        //        }
        //        if (folder.Name == "Inbox")
        //        {
        //            it.ItemAdd += InboxAdded;
        //        }
        //        else
        //        {
        //            it.ItemAdd += SubInboxAdded;
        //        }

        //        it.ItemChange += Items_ItemChange;
        //        listOfItems.Add(it);
        //    }
        //}
        private void PerformCalculation2()
        {
            Outlook.Items mainInbox = Application.Session.DefaultStore.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Items;
            mainInbox.ItemRemove += InboxItemRemovedProcessed;
            mainInbox.ItemAdd += InboxAdded;
            mainInbox.ItemChange += Items_ItemChange;
            listOfItems.Add(mainInbox);

            //FindAllInboxFolders(Application.Session.Folders);
            FindAllInboxFolders(Application.Session.DefaultStore.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Folders);
            foreach (Outlook.Folder folder in this.InboxFolders)
            {
                Outlook.Items it = folder.Items;
                if (folder.Name == "Inbox")
                {
                    //  it.ItemRemove += InboxItemRemovedProcessed;
                }
                else
                {
                    it.ItemRemove += SubInboxRemoved;
                }
                if (folder.Name == "Inbox")
                {
                    //it.ItemAdd += InboxAdded;
                }
                else
                {
                    it.ItemAdd += SubInboxAdded;
                }

                it.ItemChange += Items_ItemChange;
                listOfItems.Add(it);
            }
        }

        private void SubInboxAdded(object Item)
        {
            emailManager.SubInboxAdded();
            UpdateLabel();
        }

        private void InboxAdded(object Item)
        {
            emailManager.InboxAdded();
            UpdateLabel();
        }

        private void SubInboxRemoved()
        {
            emailManager.SubInboxRemoved();
            UpdateLabel();
        }

        private void InboxItemRemovedProcessed()
        {
            emailManager.InboxItemRemovedProcessed();
            UpdateLabel();
        }

        private void SentItems()
        {
            Outlook.MAPIFolder sent = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
            sentItems = sent.Items;
            sentItems.ItemAdd += SentItems_ItemAdd;
        }

        private void SentItems_ItemAdd(object Item)
        {
            emailManager.SentItems_ItemAdd();
        }

        private void Items_ItemChange(object Item)
        {
            // PerformChange(() => this.DetailsItems.TaskCountAdded++);
            //var i = Item as Outlook.MailItem;
            //if(i!=null)
            //{
            //    string s = (i.Parent as Outlook.Folder).Name;
            //}
        }





        //private bool CheckIfRemovedElementExist()
        //{
        //    if (this.mailElements.Where(x=>x.AddedDate.AddSeconds(5)>DateTime.Now).ToList().Count>0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}



        private void FindAllInboxFolders(Outlook.Folders folders)
        {
            foreach (Outlook.Folder folder in folders)
            {
                if (folder.Folders.Count > 0)
                {
                    FindAllInboxFolders(folder.Folders);
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

        private void FillDetailListDB()
        {
            //DB.mBoxEntities entities = new DB.mBoxEntities();
            //entities.CalculateEmails

        }





        private void Button1_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {

        }

        //private void InboxFolderItemAdded(object Item)
        //{
        //    if (Item is Outlook.MailItem)
        //    {
        //        NewInboxItem();
        //    }
        //}


        private void UpdateLabel()
        {
            UpdateLabel(emailManager.TodayCalculationDetails);
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
