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

        // This is needed for when we setup role.
        //public Helpers.Role Role { get; set; }

        //Trying to display/send over details for a forbind list showing
        //public Customer Customer { get; set; }
    }
}
