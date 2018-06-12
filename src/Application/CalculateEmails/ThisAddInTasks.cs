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
using CalculateEmails.ServiceClient;

namespace CalculateEmails
{
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

        private void TaskItems_ItemChange(object Item)
        {
            Outlook.MailItem element = Item as Outlook.MailItem;
            if (element != null)
            {
                var x = element.FlagStatus;
                if (element.FlagStatus == Microsoft.Office.Interop.Outlook.OlFlagStatus.olFlagComplete)
                { 
                    
                    UpdateLabel(new WcfClient().ProcesOutlookTask(TaskActionType.Changed));
                    //manager.TaskItems_ItemChange(Item);       
                }
            }
        }

        private void TaskItems_ItemRemove()
        {
            UpdateLabel(new WcfClient().ProcesOutlookTask(TaskActionType.Removed));
        }

        private void TaskItems_ItemAdd(object Item)
        {
            UpdateLabel(new WcfClient().ProcesOutlookTask(TaskActionType.Added));
        }
    }
}
