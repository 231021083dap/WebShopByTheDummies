using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class NewProduct
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        //[Required]
        //public NewImage Image { get; set; }
    }
}
