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
        private static ConcurrentDictionary<Guid, MailElement> mailElements = new ConcurrentDictionary<Guid, MailElement>();
        private ConcurrentBag<ActionItem> list = new ConcurrentBag<ActionItem>();

        public void Process(EmailActionType processingType, InboxType doneIn)
        {
            WriteToLog($"[Process Start] ");
            PrintMailElements(mailElements);
            WriteToLog($"{processingType} {doneIn} ");
     



            this.list.Add(new ActionItem() { Id = 11, DoneIn = InboxType.Main, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseMailCount });
            this.list.Add(new ActionItem() { Id = 12, DoneIn = InboxType.Main, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.Removed, PreviousDoneIn = InboxType.Subinbox, ThenPerform = DecreaseProcessed });
            this.list.Add(new ActionItem() { Id = 13, DoneIn = InboxType.Main, Action = EmailActionType.Removed, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseProcessed });

            this.list.Add(new ActionItem() { Id = 21, DoneIn = InboxType.Subinbox, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = DecreaseProcessed });
            this.list.Add(new ActionItem() { Id = 22, DoneIn = InboxType.Subinbox, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.Removed, PreviousDoneIn = InboxType.Main, ThenPerform = DecreaseProcessed });
            this.list.Add(new ActionItem() { Id = 23, DoneIn = InboxType.Subinbox, Action = EmailActionType.Removed, AndPreviousAction = EmailActionType.Added, PreviousDoneIn = InboxType.Main, ThenPerform = DecreaseMailCount });
            this.list.Add(new ActionItem() { Id = 24, DoneIn = InboxType.Subinbox, Action = EmailActionType.Removed, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseProcessed });


            this.list.Add(new ActionItem() { Id = 31, DoneIn = InboxType.Sent, Action = EmailActionType.Added, AndPreviousAction = EmailActionType.None, PreviousDoneIn = InboxType.None, ThenPerform = IncreaseSentItem });

            
            



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

                    // WriteToLog($"Found item {item.Id}");


                    if (element.Value == null && item.AndPreviousAction == EmailActionType.None)
                    {
                        WriteToLog($"Perform action: {item.Id}");
                        item.ThenPerform();
      
                    }
                    if (element.Value != null && item.AndPreviousAction != EmailActionType.None)
                    {
                        WriteToLog($"Perform action: {item.Id}");
                        MailElement x;
                        mailElements.TryRemove(element.Key, out x);
                        item.ThenPerform();
             
                    }
                }
            }

            mailElements.TryAdd(Guid.NewGuid(), new MailElement(processingType, doneIn));
            PrintMailElements(mailElements);
            WriteToLog($"[Process End]");
        }

        private void PrintMailElements(ConcurrentDictionary<Guid, MailElement> mailElements)
        {
            WriteToLog("    [MailElementList]");
            foreach (var item in mailElements)
            {
                WriteToLog($"   {item.Value.PreviousAction} {item.Value.PreviousDoneIn} {item.Value.AddedDate}");
            }
            WriteToLog("    [MailElementList]");
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
