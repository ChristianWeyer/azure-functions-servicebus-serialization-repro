package com.thinktecture.serverless.functions;

import java.io.IOException;
import java.util.Date;
import java.util.UUID;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.microsoft.azure.functions.ExecutionContext;
import com.microsoft.azure.functions.annotation.FunctionName;
import com.microsoft.azure.functions.annotation.ServiceBusQueueOutput;
import com.microsoft.azure.functions.annotation.ServiceBusQueueTrigger;
import com.thinktecture.serverless.messages.NewOrderMessage;
import com.thinktecture.serverless.messages.ShippingCreatedMessage;

public class CreateShipment {
    /**
     * @throws InterruptedException
     * @throws IOException
     * @throws JsonMappingException
     * @throws JsonParseException
     */
    @FunctionName("CreateShipment")
    @ServiceBusQueueOutput(name = "$return",
        queueName = "shippingsinitiated",
        connection = "ServiceBus")
    public ShippingCreatedMessage run(
            @ServiceBusQueueTrigger(name = "message", 
                queueName = "neworders", 
                connection = "ServiceBus")
            NewOrderMessage message,
            
            final ExecutionContext context
    ) throws InterruptedException, JsonParseException, JsonMappingException, IOException {
        context.getLogger().info("CreateShipment ServiceBus topic trigger function processed message.");
        context.getLogger().info(message.Order.Description);

        // NOTE: Look at our complex business logic!
        // TODO: Yes - do the REAL STUFF here...
        Thread.sleep(5000);

        ShippingCreatedMessage shippingCreated = new ShippingCreatedMessage()
        {{
            Id = UUID.randomUUID();
            Created = new Date();
            OrderId = message.Order.Id;
        }};

        context.getLogger().info("New shipment: " + shippingCreated.OrderId);

        return shippingCreated;
    }
}
