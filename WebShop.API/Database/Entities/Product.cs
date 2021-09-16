using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Category.Id")]
        public int CategoryId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey("Image.Id")]
        public int ImageId { get; set; }

        public List<Image> Image { get; set; }

        public Category Category { get; set; }
    }
}
