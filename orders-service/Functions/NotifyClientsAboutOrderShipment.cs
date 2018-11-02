using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serverless.Messages;

namespace Serverless
{
    public static class NotifyClientsAboutOrderShipmentFunction
    {
        [FunctionName("NotifyClientsAboutOrderShipment")]
        public static void Run(
            [ServiceBusTrigger("shippingsinitiated", Connection = "ServiceBus")]
            /*ShippingCreatedMessage*/ string msg, // Issue with deserializing incoming message

            ILogger log)
        {
            log.LogInformation($"NotifyClientsAboutOrderShipment SB queue trigger function processed message: {msg}");

            ShippingCreatedMessage message = JsonConvert.DeserializeObject<ShippingCreatedMessage>(msg);
            
            var messageToNotify = new { orderId = message.OrderId };

            // TODO: Notify users' clients through SignalR...
        }
    }
}
