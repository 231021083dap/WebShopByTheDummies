using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.API.Database.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Picture { get; set; }

        public List<Product> products { get; set; } = new();
    }
}
