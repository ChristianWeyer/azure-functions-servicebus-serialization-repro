
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus;
using AutoMapper;

namespace Serverless
{
    public static class SubmitNewOrderFunction
    {
        static SubmitNewOrderFunction()
        {
            InitializeMapper();
        }

        [FunctionName("SubmitNewOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "orders")]
            //DTOs.Order newOrder, // there is a bug with deserialization: https://github.com/Azure/azure-functions-host/issues/3370
            HttpRequest req,

            [ServiceBus("neworders", Connection="ServiceBus")]
            IAsyncCollector<Messages.NewOrderMessage> messages,

            ILogger log)
        {
            log.LogInformation("SubmitNewOrder HTTP trigger function processed a request.");

            var newOrder = req.Deserialize<DTOs.Order>();
            newOrder.Id = Guid.NewGuid();
            newOrder.Created = DateTime.UtcNow;

            var newOrderMessage = new Messages.NewOrderMessage
            {
                Order = Mapper.Map<Entities.Order>(newOrder)
            };

            try
            {
                log.LogInformation("Sending new order message to Queue: {0}", newOrderMessage);
                await messages.AddAsync(newOrderMessage);
                await messages.FlushAsync();
            }
            catch (ServiceBusException sbx)
            {
                // TODO: retry policy...
                log.LogError(sbx, "Service Bus Error");
                throw;
            }

            return new OkResult();
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DTOs.OrderItem, Entities.OrderItem>();
                cfg.CreateMap<DTOs.Order, Entities.Order>()
                    .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items));
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
