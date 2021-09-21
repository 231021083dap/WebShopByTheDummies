using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.API.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Helpers.Role Role { get; set; }

        public Customer Customer { get; set; }

        //public List<Customer> Customers { get; set; } = new();
    }
}
