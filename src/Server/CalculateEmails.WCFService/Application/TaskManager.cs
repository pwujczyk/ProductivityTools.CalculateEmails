using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.WCFService.Application
{
    class TaskManager : BaseManager
    {
        public void TaskItems_ItemRemove()
        {
            PerformChange(() => this.TodayCalculationDetails.TaskCountRemoved++);
        }

        public void TaskItems_ItemAdd()
        {
            PerformChange(() => this.TodayCalculationDetails.TaskCountAdded++);
        }

        public void TaskItems_ItemChange()
        {
            PerformChange(() => this.TodayCalculationDetails.TaskCountFinished++);
        }
    }
}
