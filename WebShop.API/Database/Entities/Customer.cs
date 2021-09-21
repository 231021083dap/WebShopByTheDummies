using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class Customer
    {
        [ForeignKey("User.Id")]
        public int UserId { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public User User { get; set; }
        //public Address Address { get; set; }
        //public ZipCity ZipCity { get; set; }
    }
}
