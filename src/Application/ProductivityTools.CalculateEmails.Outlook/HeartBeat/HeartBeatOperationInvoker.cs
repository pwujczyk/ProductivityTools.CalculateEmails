using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Contract.Arch
{
    public class HeartBeatOperationInvoker : IOperationInvoker
    {
        IOperationInvoker OriginalInvoker;
        public bool IsSynchronous => OriginalInvoker.IsSynchronous;

        public HeartBeatOperationInvoker(IOperationInvoker orginalInvoker)
        {
            this.OriginalInvoker = orginalInvoker;
        }

        public object[] AllocateInputs()
        {
            return OriginalInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            return OriginalInvoker.Invoke(instance, inputs, out outputs);
            //outputs = new object[0];
            //return false;
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return OriginalInvoker.InvokeBegin(instance, inputs, callback, state);

        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return OriginalInvoker.InvokeEnd(instance, out outputs, result);
        }
    }
}
