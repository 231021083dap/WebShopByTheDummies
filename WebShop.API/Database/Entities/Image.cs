using System.ComponentModel.DataAnnotations;

namespace WebShop.API.Database.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
