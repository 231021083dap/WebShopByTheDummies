using System.ComponentModel.DataAnnotations;

namespace WebShop.API.Database.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //Should be setup as the product images is
        //Don't know if string will work here?
        [Required]
        public string Picture { get; set; }
    }
}
