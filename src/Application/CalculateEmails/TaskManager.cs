using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{
    class TaskManager :BaseManager
    {
        public void TaskItems_ItemRemove()
        {
            PerformChange(() => this.TodayCalculationDetails.TaskCountRemoved++);
        }

        public void TaskItems_ItemAdd(object Item)
        {
            PerformChange(() => this.TodayCalculationDetails.TaskCountAdded++);
        }

        public void TaskItems_ItemChange(object Item)
        {
            PerformChange(() => this.TodayCalculationDetails.TaskCountFinished++);
        }
    }
}
