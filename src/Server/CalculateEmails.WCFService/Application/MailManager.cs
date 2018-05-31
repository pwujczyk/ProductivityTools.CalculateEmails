using CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.WCFService.Application
{
    delegate void MailAction();

    class BLManager : BaseManager
    {




        private int secondsdelay = 200000;
        private static ConcurrentDictionary<Guid,MailElement> mailElements = new ConcurrentDictionary<Guid,MailElement>();
        private ConcurrentBag<ActionItem> list = new ConcurrentBag<ActionItem>();

        public void Process(EmailActionType processingType, InboxType doneIn)
        {
            WriteToLog($"{processingType} {doneIn} ");
            mailElements.TryAdd(Guid.NewGuid(), new MailElement(processingType, doneIn));

            this.list.Add(new ActionItem() { Action = EmailActionType.Added, DoneIn = InboxType.Main, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseMailCount });
            this.list.Add(new ActionItem() { Action = EmailActionType.Added, DoneIn = InboxType.Sent, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseSentItem });

            this.list.Add(new ActionItem() { Action = EmailActionType.Removed, DoneIn = InboxType.Main, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseProcessed });
            this.list.Add(new ActionItem() { Action = EmailActionType.Added, DoneIn = InboxType.Subinbox, AndPreviousAction = EmailActionType.Removed, PreviousDoneIn = InboxType.Main, ThenPerform = DecreaseProcessed });

            this.list.Add(new ActionItem() { Action = EmailActionType.Removed, DoneIn = InboxType.Subinbox, AndPreviousAction = EmailActionType.Added, PreviousDoneIn = InboxType.Main, ThenPerform = DecreaseMailCount });

            this.list.Add(new ActionItem() { Action = EmailActionType.Added, DoneIn = InboxType.Main, AndPreviousAction = EmailActionType.Removed, PreviousDoneIn = InboxType.Subinbox, ThenPerform = DoNothing });



            //List<ActionItem> withoutHistoryList = this.list.Where(x => x.AndPreviousAction == EmailActionType.None).ToList();
            //List<ActionItem> withtHistoryList = this.list.Where(x => x.AndPreviousAction != EmailActionType.None).ToList();


            foreach (var item in list)
            {
                if (item.Action == processingType && item.DoneIn == doneIn)
                {

                    var element = mailElements.FirstOrDefault(x =>
                                    x.Value.PreviousAction == item.AndPreviousAction
                                    && x.Value.PreviousDoneIn == item.PreviousDoneIn
                                    //&& x.AddedDate.AddSeconds(secondsdelay) > DateTime.Now
                                    );
                    if (element.Value == null && item.AndPreviousAction == EmailActionType.None)
                    {

                        item.ThenPerform();
                    }
                    if (element.Value != null && item.AndPreviousAction != EmailActionType.None)
                    {
                        MailElement x;
                        mailElements.TryRemove(element.Key, out x);
                        item.ThenPerform();
                    }
                }
            }

        }

        //private bool CheckIfElementExist(EmailActionType type, InboxType subinbox)
        //{
        //    var element = this.mailElements.FirstOrDefault(x => x.PreviousAction == type && x.PreviousDoneIn == subinbox && x.AddedDate.AddSeconds(500) > DateTime.Now);
        //    if (element != null)
        //    {
        //        this.mailElements.Remove(element);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void InboxAdded()
        //{
        //    this.Process(EmailActionType.Added, InboxType.Main);

        //    return;
        //    if (CheckIfElementExist(EmailActionType.Removed, InboxType.Subinbox))
        //    {
        //        //DecreaseMailCount();
        //        DecreaseProcessed();
        //    }
        //    else
        //    {
        //        IncreaseMailCount();
        //        this.mailElements.Add(new MailElement(EmailActionType.Added, InboxType.Main));
        //    }
        //}

        //public void InboxItemRemovedProcessed()
        //{
        //    this.Process(EmailActionType.Removed, InboxType.Main);
        //    return;
        //    if (CheckIfElementExist(EmailActionType.Processed, InboxType.Subinbox))
        //    {
        //        //IncreaseMailCount();
        //        //do nothing
        //        DecreaseProcessed();
        //    }
        //    else
        //    {
        //        IncreaseProcessed();
        //        this.mailElements.Add(new MailElement(EmailActionType.Processed, InboxType.Main));
        //    }
        //}

        //public void SubInboxRemoved()
        //{
        //    return;
        //    if (CheckIfElementExist(EmailActionType.Added, InboxType.Main))
        //    {
        //        DecreaseMailCount();

        //    }
        //    else
        //    {
        //        IncreaseProcessed();
        //        this.mailElements.Add(new MailElement(EmailActionType.Removed, InboxType.Subinbox));
        //    }
        //}

        //public void SubInboxAdded()
        //{
        //    this.Process(EmailActionType.Added, InboxType.Subinbox);
        //    return;
        //    if (CheckIfElementExist(EmailActionType.Processed, InboxType.Main))
        //    {
        //        DecreaseProcessed();
        //    }
        //    else
        //    {
        //        this.mailElements.Add(new MailElement(EmailActionType.Added, InboxType.Subinbox));
        //    }
        //}

        public void SentItems_ItemAdd()
        {
            return;
            IncreaseSentItem();
        }

        private void DoNothing() { }

        private void IncreaseMailCount()
        {
            WriteToLog($"IncreaseMailCount");
            PerformChange(() => this.TodayCalculationDetails.MailCountAdd++);
        }

        private void IncreaseProcessed()
        {
            WriteToLog($"IncreaseProcessed");
            PerformChange(() => this.TodayCalculationDetails.MailCountProcessed++);
        }

        private void DecreaseProcessed()
        {
            WriteToLog($"DecreaseProcessed");
            PerformChange(() => this.TodayCalculationDetails.MailCountProcessed--);
        }

        private void DecreaseMailCount()
        {
            WriteToLog($"DecreaseMailCount");
            PerformChange(() => this.TodayCalculationDetails.MailCountAdd--);
        }

        private void IncreaseSentItem()
        {
            WriteToLog($"IncreaseSentItem");
            PerformChange(() => this.TodayCalculationDetails.MailCountSent++);
        }







    }
}
