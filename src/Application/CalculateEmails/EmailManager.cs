using CalculateEmails.Actions;
using DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{

    delegate void MailAction();

    class BLManager : BaseManager
    {

        private int secondsdelay = 10;
        List<MailElement> mailElements = new List<MailElement>();
        private List<ActionItem> list = new List<ActionItem>();

        private void Process(ActionType processingType, InboxType doneIn)
        {
            this.list.Add(new ActionItem() { Action = ActionType.Added, DoneIn = InboxType.Main, AndPreviousAction = ActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseMailCount });
            this.list.Add(new ActionItem() { Action = ActionType.Added, DoneIn = InboxType.Main, AndPreviousAction = ActionType.Removed, PreviousDoneIn = InboxType.Subinbox, ThenPerform = DoNothing });

            this.list.Add(new ActionItem() { Action = ActionType.Removed, DoneIn = InboxType.Main, AndPreviousAction = ActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseProcessed });

            this.list.Add(new ActionItem() { Action = ActionType.Added, DoneIn = InboxType.Subinbox, AndPreviousAction = ActionType.Removed, PreviousDoneIn = InboxType.Main, ThenPerform = DecreaseProcessed });

            foreach (var item in list)
            {
                if (item.Action == processingType && item.DoneIn == doneIn)
                {
                    var element = this.mailElements.FirstOrDefault(x =>
                                    x.PreviousAction == item.AndPreviousAction
                                    && x.PreviousDoneIn == item.PreviousDoneIn
                                    && x.AddedDate.AddSeconds(secondsdelay) > DateTime.Now);
                    if (element != null)
                    {
                        item.ThenPerform();
                    }
                }
            }
        }

        private bool CheckIfElementExist(ActionType type, InboxType subinbox)
        {
            var element = this.mailElements.FirstOrDefault(x => x.PreviousAction == type && x.PreviousDoneIn == subinbox && x.AddedDate.AddSeconds(500) > DateTime.Now);
            if (element != null)
            {
                this.mailElements.Remove(element);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InboxAdded()
        {
            this.Process(ActionType.Added, InboxType.Main);

            return;
            if (CheckIfElementExist(ActionType.Removed, InboxType.Subinbox))
            {
                //DecreaseMailCount();
                DecreaseProcessed();
            }
            else
            {
                IncreaseMailCount();
                this.mailElements.Add(new MailElement(ActionType.Added, InboxType.Main));
            }
        }

        public void InboxItemRemovedProcessed()
        {
            this.Process(ActionType.Removed, InboxType.Main);
            return;
            if (CheckIfElementExist(ActionType.Processed, InboxType.Subinbox))
            {
                //IncreaseMailCount();
                //do nothing
                DecreaseProcessed();
            }
            else
            {
                IncreaseProcessed();
                this.mailElements.Add(new MailElement(ActionType.Processed, InboxType.Main));
            }
        }

        public void SubInboxRemoved()
        {
            return;
            if (CheckIfElementExist(ActionType.Added, InboxType.Main))
            {
                DecreaseMailCount();

            }
            else
            {
                IncreaseProcessed();
                this.mailElements.Add(new MailElement(ActionType.Removed, InboxType.Subinbox));
            }
        }

        public void SubInboxAdded()
        {
            this.Process(ActionType.Added, InboxType.Subinbox);
            return;
            if (CheckIfElementExist(ActionType.Processed, InboxType.Main))
            {
                DecreaseProcessed();
            }
            else
            {
                this.mailElements.Add(new MailElement(ActionType.Added, InboxType.Subinbox));
            }
        }

        public void SentItems_ItemAdd()
        {
            return;
            NewSentItem();
        }

        private void DoNothing() { }

        private void IncreaseMailCount()
        {
            PerformChange(() => this.TodayCalculationDetails.MailCountAdd++);
        }

        private void IncreaseProcessed()
        {
            PerformChange(() => this.TodayCalculationDetails.MailCountProcessed++);
        }

        private void DecreaseProcessed()
        {
            PerformChange(() => this.TodayCalculationDetails.MailCountProcessed--);
        }

        private void DecreaseMailCount()
        {
            PerformChange(() => this.TodayCalculationDetails.MailCountAdd--);
        }

        private void NewSentItem()
        {
            PerformChange(() => this.TodayCalculationDetails.MailCountSent++);
        }







    }
}
