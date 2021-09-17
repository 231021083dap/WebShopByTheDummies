using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [ForeignKey("Product.Id")]
        public int productId { get; set; }
    }
}
