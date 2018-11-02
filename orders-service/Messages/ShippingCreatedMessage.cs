using System;

namespace Serverless.Messages
{
    public class ShippingCreatedMessage
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid OrderId { get; set; }
    }
}
