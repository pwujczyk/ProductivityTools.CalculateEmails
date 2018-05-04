using CalculateEmails.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{
  

    class MailElement
    {
        public DateTime AddedDate { get; set; }
        public ActionType PreviousAction { get; set; }
        public InboxType PreviousDoneIn { get; set; }

        public MailElement(ActionType processingType,InboxType inboxType)
        {
            this.AddedDate = DateTime.Now;
            this.PreviousAction = processingType;
            this.PreviousDoneIn = inboxType;
        }
    }
}
