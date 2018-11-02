package com.thinktecture.serverless.dtos;

import java.util.Date;
import java.util.List;
import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonFormat;

public class Order {
   public UUID Id;
   public String Description;
   @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "yyyy-MM-dd'T'HH:mm:ss.SSSSSS'Z'")
   public Date Created;
   public List<OrderItem> Items;
}