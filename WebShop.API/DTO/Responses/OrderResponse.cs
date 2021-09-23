using System;
using System.Collections.Generic;

namespace WebShop.API.DTO.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public AddressResponse BillingAddress { get; set; }
        public AddressResponse ShipmentAddress { get; set; }
        public List<OrderItemOrderResponse> OrderItems { get; set; }
    }
    public class OrderItemOrderResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int CurrentPrice { get; set; }
    }
}
