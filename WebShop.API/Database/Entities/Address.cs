using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class Address
    {
        [ForeignKey("Customer.Id")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public int Number { get; set; }

        public string Floor { get; set; }

        [ForeignKey("ZipCity.Id")]
        public int ZipCityId { get; set; }

        public ZipCity ZipCity { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
