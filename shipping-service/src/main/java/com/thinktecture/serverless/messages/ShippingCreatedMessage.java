package com.thinktecture.serverless.messages;

import java.util.Date;
import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonFormat;

public class ShippingCreatedMessage {
   public UUID Id;
   @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "yyyy-MM-dd'T'HH:mm:ss.SSSSSS'Z'")
   public Date Created;
   public UUID OrderId;
}