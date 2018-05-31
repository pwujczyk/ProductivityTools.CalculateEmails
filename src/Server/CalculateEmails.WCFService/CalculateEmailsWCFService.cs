using AutoMapper;
using CalculateEmails.Contract;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.Contract.ServiceContract;
using CalculateEmails.WCFService.Application;
using DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculateEmails.WCFService
{
    public class CalculateEmailsWCFService : ICalculateEmailsWCFMQService
    {
        public CalculateEmailsWCFService()
        {
        }

        public void ProcessMail(InboxType inboxType, EmailActionType actionType)
        {
            PerformOperation(inboxType, actionType);
        }

        private void PerformOperation(InboxType inboxType, EmailActionType actionType)
        {
            BLManager bLManager = new BLManager();
            bLManager.Process(actionType, inboxType);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CalculationDayDB, CalculationDay>());
            IMapper iMapper = config.CreateMapper();
            var x = iMapper.Map<CalculationDayDB, CalculationDay>(bLManager.TodayCalculationDetails);
        }

        public void ProcessTask(TaskActionType taskActionType)
        {
            //return new CalculationDay();
        }

        public void HeartBeat()
        {
            //return true;
        }
    }
}
