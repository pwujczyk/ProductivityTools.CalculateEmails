using ProductivityTools.CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.WCFService.Application
{
    public class TaskManager : BaseManager
    {

        public void Process(TaskActionType processingType, DateTime date)
        {
            switch (processingType)
            {
                case TaskActionType.Finished:
                    TaskCompleted(date);
                    break;
                case TaskActionType.Removed:
                    TaskRemoved(date);
                    break;
                case TaskActionType.Added:
                    TaskAdded(date);
                    break;
                default:
                    break;
            }
        }

        public void TaskRemoved(DateTime date)
        {
            PerformChange(date, (calculationDayDB) => calculationDayDB.TaskCountRemoved++);
        }

        public void TaskAdded(DateTime date)
        {
            PerformChange(date, (calculationDayDB) => calculationDayDB.TaskCountAdded++);
        }

        public void TaskCompleted(DateTime date)
        {
            PerformChange(date,(calculationDayDB) => calculationDayDB.TaskCountFinished++);
        }
    }
}
