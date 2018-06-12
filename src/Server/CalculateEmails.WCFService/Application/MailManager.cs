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

    public class BLManager : BaseManager
    {




        private int milisecondsDelay = 100;
        private static ConcurrentDictionary<Guid, MailElement> mailElements = new ConcurrentDictionary<Guid, MailElement>();
        private ConcurrentBag<ActionItem> actionList = new ConcurrentBag<ActionItem>();

        public BLManager()
        {
            this.actionList.Add(new ActionItem() { Id = 11, DoneIn = InboxType.Main, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseMailCount });

            this.actionList.Add(new ActionItem() { Id = 12, DoneIn = InboxType.Main, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.Removed, PreviousDoneIn = InboxType.Subinbox, ThenPerform = DecreaseProcessed });
            this.actionList.Add(new ActionItem() { Id = 13, DoneIn = InboxType.Main, Action = EmailActionType.Removed, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseProcessed });
            this.actionList.Add(new ActionItem() { Id = 14, DoneIn = InboxType.Main, Action = EmailActionType.Removed, AndPreviousAction = EmailActionType.Added, PreviousDoneIn = InboxType.Subinbox, ThenPerform = IncreaseProcessed});

            this.actionList.Add(new ActionItem() { Id = 21, DoneIn = InboxType.Subinbox, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = DecreaseProcessed });
            this.actionList.Add(new ActionItem() { Id = 22, DoneIn = InboxType.Subinbox, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.Removed, PreviousDoneIn = InboxType.Main, ThenPerform = DecreaseProcessed });
            this.actionList.Add(new ActionItem() { Id = 23, DoneIn = InboxType.Subinbox, Action = EmailActionType.Removed, AndPreviousAction = EmailActionType.Added, PreviousDoneIn = InboxType.Main, ThenPerform = DecreaseMailCount });
            this.actionList.Add(new ActionItem() { Id = 24, DoneIn = InboxType.Subinbox, Action = EmailActionType.Removed, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseProcessed });


            this.actionList.Add(new ActionItem() { Id = 31, DoneIn = InboxType.Sent, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseSentItem });


        }

        public void Process(EmailActionType processingType, InboxType doneIn)
        {
            WriteToLog($"[Process Start]");
            PrintMailElements(mailElements);
            WriteToLog($"{processingType} {doneIn} ");


            var actionListScoped = this.actionList.Where(x => x.DoneIn == doneIn && x.Action == processingType).ToList();
            var withPrevious = actionListScoped.Where(x => x.AndPreviousAction != EmailActionType.None);

            //List<ActionItem> withoutHistoryList = this.list.Where(x => x.AndPreviousAction == EmailActionType.None).ToList();
            //List<ActionItem> withtHistoryList = this.list.Where(x => x.AndPreviousAction != EmailActionType.None).ToList();


            foreach (var currentAction in withPrevious)
            {
                var element = mailElements.FirstOrDefault(x =>
                                x.Value.PreviousAction == currentAction.AndPreviousAction
                                && x.Value.PreviousDoneIn == currentAction.PreviousDoneIn
                                && x.Value.AddedDate.AddMilliseconds(milisecondsDelay) > DateTime.Now
                                );

                if (element.Value != null)
                {
                    WriteToLog($"Perform action: {currentAction.Id}");
                    MailElement x;
                    mailElements.TryRemove(element.Key, out x);
                    currentAction.ThenPerform();
                    mailElements.TryAdd(Guid.NewGuid(), new MailElement(processingType, doneIn));
                    return;
                }
            }

            var withoutPrevious = actionListScoped.Single(x => x.AndPreviousAction == EmailActionType.None);

            WriteToLog($"Perform action: {withoutPrevious.Id}");
            withoutPrevious.ThenPerform();
            mailElements.TryAdd(Guid.NewGuid(), new MailElement(processingType, doneIn));
            return;



        }

        private void PrintMailElements(ConcurrentDictionary<Guid, MailElement> mailElements)
        {
            WriteToLog("[MailElementList-Begin]");
            foreach (var item in mailElements)
            {
                WriteToLog($"   {item.Value.PreviousAction} {item.Value.PreviousDoneIn} {item.Value.AddedDate}");
            }
            WriteToLog("[MailElementList-End]");
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
            WriteToLog($"====Method=====IncreaseMailCount");
            PerformChange((calculationDayDB) => calculationDayDB.MailCountAdd++);
        }

        private void IncreaseProcessed()
        {
            WriteToLog($"====Method=====IncreaseProcessed");
            PerformChange((calculationDayDB) => calculationDayDB.MailCountProcessed++);
        }

        private void DecreaseProcessed()
        {
            WriteToLog($"====Method=====DecreaseProcessed");
            PerformChange((calculationDayDB) => calculationDayDB.MailCountProcessed--);
        }

        private void DecreaseMailCount()
        {
            WriteToLog($"====Method=====DecreaseMailCount");
            PerformChange((calculationDayDB) => calculationDayDB.MailCountAdd--);
        }

        private void IncreaseSentItem()
        {
            WriteToLog($"====Method=====IncreaseSentItem");
            PerformChange((calculationDayDB) => calculationDayDB.MailCountSent++);
        }







    }
}
