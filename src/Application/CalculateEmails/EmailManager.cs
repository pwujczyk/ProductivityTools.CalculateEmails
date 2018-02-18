using DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{
    class BLManager: BaseManager
    {
        
        List<MailElement> mailElements = new List<MailElement>();
        //List<CalculationDetails> Details;


        //CalculationDetails DetailsItems
        //{
        //    get
        //    {
        //        return this.Details.FirstOrDefault(x => x.Date == this.CurrentDay);
        //    }
        //}


        private bool CheckIfElementExist(ProcessingType type, InboxType subinbox)
        {
            var element = this.mailElements.FirstOrDefault(x => x.ProcessingType == type && x.SubInbox == subinbox && x.AddedDate.AddSeconds(500) > DateTime.Now);
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
            if (CheckIfElementExist(ProcessingType.removed, InboxType.subinbox))
            {
                //DecreaseMailCount();
                DecreaseProcessed();
            }
            else
            {
                IncreaseMailCount();
                this.mailElements.Add(new MailElement(ProcessingType.added, InboxType.main));
            }
        }

        public void InboxItemRemovedProcessed()
        {
            if (CheckIfElementExist(ProcessingType.processed, InboxType.subinbox))
            {
                //IncreaseMailCount();
                //do nothing
                DecreaseProcessed();
            }
            else
            {
                IncreaseProcessed();
                this.mailElements.Add(new MailElement(ProcessingType.processed, InboxType.main));
            }
        }

        public void SubInboxRemoved()
        {
            if (CheckIfElementExist(ProcessingType.added, InboxType.main))
            {
                DecreaseMailCount();

            }
            else
            {
                IncreaseProcessed();
                this.mailElements.Add(new MailElement(ProcessingType.removed, InboxType.subinbox));
            }
        }

        public void SubInboxAdded()
        {

            if (CheckIfElementExist(ProcessingType.processed, InboxType.main))
            {
                DecreaseProcessed();
            }
            else
            {
                this.mailElements.Add(new MailElement(ProcessingType.added, InboxType.subinbox));
            }
        }

        public void SentItems_ItemAdd()
        {
            NewSentItem();
        }



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
