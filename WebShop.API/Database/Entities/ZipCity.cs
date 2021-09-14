using System.ComponentModel.DataAnnotations;

namespace WebShop.API.Database.Entities
{
    public class ZipCity
    {
        [Key]
        public int ZipCode { get; set; }

        [Required]
        public string City { get; set; }
    }
}
