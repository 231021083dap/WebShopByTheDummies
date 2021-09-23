using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class UpdateCategory
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Picture { get; set; }
    }
}
