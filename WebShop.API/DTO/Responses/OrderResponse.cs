using System.Collections.Generic;

namespace WebShop.API.DTO.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public AddressResponse AddressId { get; set; }
        public List<OrderItemOrderResponse> OrderItems { get; set; }
    }
    public class OrderItemOrderResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int CurrentPrice { get; set; }
    }
}
