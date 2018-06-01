using CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.WCFService.Application
{
    class ActionItem
    {
        public int Id { get; set; }
        public EmailActionType Action { get; set; } //added removed
        public InboxType DoneIn { get; set; }
        
        public EmailActionType AndPreviousAction { get; set; }

        public InboxType PreviousDoneIn { get; set; }

        public MailAction ThenPerform { get; set; }

    }
}
