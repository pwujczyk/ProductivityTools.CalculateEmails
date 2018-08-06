using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ProductivityTools.CalculateEmails.ServiceClient;
using ProductivityTools.CalculateEmails.Contract.DataContract;

namespace ProductivityTools.CalculateEmails
{
    public partial class ThisAddIn
    {
        private bool CalculateEmailsEnabled
        {
            get
            {
                return Globals.Ribbons.CalculateEmails.chCalculateEmailsEnabled.Checked;
            }
            set
            {
                Globals.Ribbons.CalculateEmails.chCalculateEmailsEnabled.Checked = value;
            }
        }

        private void MailsManage()
        {
            RegisterMainInboxHandlers();
            FindAllInboxFolders();
            RegisterSubInboxHandlers();
            RegisterSentItemsHanlder();
        }

        private void RegisterMainInboxHandlers()
        {
            Outlook.Items mainInbox = Application.Session.DefaultStore.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Items;
            mainInbox.ItemRemove += MainInboxRemoved;
            mainInbox.ItemAdd += MainInboxAdded;
            mainInbox.ItemChange += MainInboxChanged;

            listOfItems.Add(mainInbox);
        }

        private void FindAllInboxFolders()
        {
            FindAllInboxFoldersRecursive(Application.Session.DefaultStore.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).Folders);
        }

        private void RegisterSubInboxHandlers()
        {
            foreach (Outlook.Folder folder in this.InboxFolders)
            {
                Outlook.Items folderItems = folder.Items;
                if (folder.Name != MainInboxName)
                {
                    folderItems.ItemRemove += SubInboxRemoved;
                    folderItems.ItemAdd += SubInboxAdded;
                    folderItems.ItemChange += SubInboxChanged;
                }

                listOfItems.Add(folderItems);
            }
        }

        private void RegisterSentItemsHanlder()
        {
            Outlook.MAPIFolder sent = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
            sentItems = sent.Items;
            sentItems.ItemAdd += SentItems_ItemAdd;
        }

        private void SubInboxChanged(object Item)
        {
            //throw new NotImplementedException();
        }

        private void CallerWrapper(Action a)
        {
            if (CalculateEmailsEnabled)
            {
                a();
                DelayAndUpdateLabelWrapper();
            }
        }

        private void MainInboxAdded(object Item)
        {
            CallerWrapper(() => new ProcessingClient().ProcessOutlookMail(InboxType.Main, EmailActionType.Added));
        }

        private void SubInboxAdded(object Item)
        {
            CallerWrapper(() => new ProcessingClient().ProcessOutlookMail(InboxType.Subinbox, EmailActionType.Added));
        }

        private void MainInboxRemoved()
        {
            CallerWrapper(() =>
            {
                if (InvitationsCounter > 0)
                {
                    InvitationsCounter--;
                }
                else
                {
                    new ProcessingClient().ProcessOutlookMail(InboxType.Main, EmailActionType.Removed);
                }
            });
        }

        private void SubInboxRemoved()
        {
            CallerWrapper(() => new ProcessingClient().ProcessOutlookMail(InboxType.Subinbox, EmailActionType.Removed));
        }


        private void SentItems_ItemAdd(object Item)
        {
            CallerWrapper(() => new ProcessingClient().ProcessOutlookMail(InboxType.Sent, EmailActionType.Added));
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
    }
}
