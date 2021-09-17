using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User.Id")]
        public int UserId { get; set; }
        public User user { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }
        

        //Needs service to work if needed
        //public List<User> Users { get; set; }
    }
}
