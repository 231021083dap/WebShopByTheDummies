namespace WebShop.API.DTO.Responses
{
    public class OrderItemResponse
    {
        public int Id { get; set; }
        public OrderResponse OrderId { get; set; }
        public ProductResponse Product { get; set; }
        public int Amount { get; set; }
        public int CurrentPrice { get; set; }
    }
}
