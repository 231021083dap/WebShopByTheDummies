using System.ComponentModel.DataAnnotations;

namespace WebShop.API.Database.Entities
{
    public class ZipCity
    {
        [Key]
        public int Zipcode { get; set; }

        [Required]
        public string City { get; set; }
    }
}
