using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.Actions
{
    class ActionItem
    {
        public ActionType Action { get; set; } //added removed
        public InboxType DoneIn { get; set; }
        
        public ActionType AndPreviousAction { get; set; }

        public InboxType PreviousDoneIn { get; set; }

        public MailAction ThenPerform { get; set; }

    }
}
