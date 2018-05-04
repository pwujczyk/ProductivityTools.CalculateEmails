using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.Actions
{

    enum ActionType
    {
        Added,
        Removed,
        Processed,
        None
    }

    enum InboxType
    {
        Main,
        Subinbox,
        None
    }
}
