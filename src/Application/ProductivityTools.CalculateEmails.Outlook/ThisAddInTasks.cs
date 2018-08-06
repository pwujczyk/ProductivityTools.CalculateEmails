using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using ProductivityTools.CalculateEmails.ServiceClient;
using ProductivityTools.CalculateEmails.Contract.DataContract;

namespace ProductivityTools.CalculateEmails
{
    //todo: move it to different file
    class TaskItem
    {
        private int milisecods = 200;
        DateTime Now
        {
            get
            {
                //todo: change it
                return DateTime.Now;
            }
        }

        public TaskItem()
        {
            this.Created = Now;
        }

        private DateTime Created;

        public string EntryId { get; set; }

        public bool OutDated
        {
            get
            {
                return this.Created.AddMilliseconds(milisecods) > Now;
            }
        }
    }

    public partial class ThisAddIn
    {

        //TaskManager manager = new TaskManager();

        private void TodoManage()
        {
            Outlook.MAPIFolder task = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderToDo);
            todoItems = task.Items;
            todoItems.ItemAdd += TaskItems_ItemAdd;
            todoItems.ItemRemove += TaskItems_ItemRemove;
            todoItems.ItemChange += TaskItems_ItemChange;
        }

        List<TaskItem> TaskItemsList = new List<TaskItem>();

        private void TaskItems_ItemChange(object Item)
        {
            CallerWrapper(() =>
            {
                this.TaskItemsList.RemoveAll(x => x.OutDated);
                MailItem element = Item as MailItem;
                if (element != null)
                {
                    if (element.FlagStatus == OlFlagStatus.olFlagComplete)
                    {
                        if (this.TaskItemsList.Any(x => x.EntryId == element.EntryID) == false)
                        {
                            this.TaskItemsList.Add(new TaskItem { EntryId = element.EntryID });
                            new ProcessingClient().ProcesOutlookTask(TaskActionType.Finished);
                        }
                    }
                }
            });
        }

        private void TaskItems_ItemRemove()
        {
            CallerWrapper(() => new ProcessingClient().ProcesOutlookTask(TaskActionType.Removed));
        }

        private void TaskItems_ItemAdd(object Item)
        {
            CallerWrapper(() => new ProcessingClient().ProcesOutlookTask(TaskActionType.Added));
        }
    }
}
