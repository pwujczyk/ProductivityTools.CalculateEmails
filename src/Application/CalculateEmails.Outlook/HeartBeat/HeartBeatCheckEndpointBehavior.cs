using CalculateEmails.Contract.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{
    public class HeartBeatCheckEndpointAttribute : Attribute, IEndpointBehavior, IClientMessageInspector
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (DateTime.Now.Minute % 3 == 0)
            {
                channel.Close();
            }
            if (false)
            {

            }
            if (DateTime.Now.Minute % 3 ==1)
            {
                channel.Abort();
            }
            if (DateTime.Now.Minute % 3 == 2)
            {
                request.Close();
            }
            return null;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
