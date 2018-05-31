using CalculateEmails.Contract.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{
    public class HeartBeatCheckAttribute : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
           // clientOperation.BeginMethod 
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new HeartBeatOperationInvoker(dispatchOperation.Invoker);
        }

        public void Validate(OperationDescription operationDescription)
        {
           
        }
    }
}
