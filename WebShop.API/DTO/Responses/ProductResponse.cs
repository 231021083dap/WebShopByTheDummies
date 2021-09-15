namespace WebShop.API.DTO.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryResponse CatagoryId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public ImageResponse ImageId { get; set; }
    }
}
