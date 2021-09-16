using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class UpdateProduct
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Description { get; set; }

        //This only works if we wanna use 1 picture at the time
        [Required]
        public int ImageId { get; set; }
    }
}
