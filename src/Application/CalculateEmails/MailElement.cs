using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{
    enum ProcessingType
    {
        added,
        removed,
        processed,
    }

    enum InboxType
    {
        main,
        subinbox
    }

    class MailElement
    {
        public DateTime AddedDate { get; set; }
        public ProcessingType ProcessingType { get; set; }
        public InboxType SubInbox { get; set; }

        public MailElement(ProcessingType processingType,InboxType inboxType)
        {
            this.AddedDate = DateTime.Now;
            this.ProcessingType = processingType;
            this.SubInbox = inboxType;
        }
    }
}
