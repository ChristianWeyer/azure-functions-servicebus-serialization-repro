using Serverless.Entities;

namespace Serverless.Messages
{
    public class NewOrderMessage
    {
        public Order Order { get; set; }
    }
}
