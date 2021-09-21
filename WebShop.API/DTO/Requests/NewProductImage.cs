using System.ComponentModel.DataAnnotations;


namespace WebShop.API.DTO.Requests
{
    public class NewProductImage
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Path { get; set; }
    }
}