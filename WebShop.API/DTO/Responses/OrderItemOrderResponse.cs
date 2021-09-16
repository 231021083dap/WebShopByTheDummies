namespace WebShop.API.DTO.Responses
{
    public class OrderItemOrderResponse
    {
        public int Id { get; set; }
        public ProductResponse ProductId { get; set; }
        public int Amount { get; set; }
        public int CurrentPrice { get; set; }
    }
}
