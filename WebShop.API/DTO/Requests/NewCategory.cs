using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class NewCategory
    {
        [Required]
        public string Name { get; set; }

        //Path (We are only using 1 picture for the category section)
        [Required]
        public string Picture { get; set; }
    }
}
