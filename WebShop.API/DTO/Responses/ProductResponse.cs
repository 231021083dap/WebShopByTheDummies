using System.Collections.Generic;

namespace WebShop.API.DTO.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductCategoryResponse Category { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public List<ProductImageResponse> Images { get; set; }
    }
    public class ProductImageResponse
    {
        public int imageId { get; set; }
        public int productId { get; set; }
        public string Path { get; set; }
    }

    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
