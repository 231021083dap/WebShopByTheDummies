using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class NewCategory
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Picture { get; set; }
    }
}
