using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class UpdateAddress
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Email must be less than 128 chars")]
        public string StreetName { get; set; }

        [Required]
        public int Number { get; set; }

        public string Floor { get; set; }

        [Required]
        public int Zipcode { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
