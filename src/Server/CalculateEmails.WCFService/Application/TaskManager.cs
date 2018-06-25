using CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.WCFService.Application
{
    class TaskManager : BaseManager
    {

        public void Process(TaskActionType processingType, DateTime date)
        {
            switch (processingType)
            {
                case TaskActionType.Changed:
                    TaskItems_ItemChange(date);
                    break;
                case TaskActionType.Removed:
                    TaskItems_ItemRemove(date);
                    break;
                case TaskActionType.Added:
                    TaskItems_ItemAdd(date);
                    break;
                default:
                    break;
            }
        }

        public void TaskItems_ItemRemove(DateTime date)
        {
            PerformChange(date, (calculationDayDB) => calculationDayDB.TaskCountRemoved++);
        }

        public void TaskItems_ItemAdd(DateTime date)
        {
            PerformChange(date, (calculationDayDB) => calculationDayDB.TaskCountAdded++);
        }

        public void TaskItems_ItemChange(DateTime date)
        {
            PerformChange(date,(calculationDayDB) => calculationDayDB.TaskCountFinished++);
        }
    }
}
