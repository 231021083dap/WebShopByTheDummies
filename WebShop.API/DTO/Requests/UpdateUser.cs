using System.ComponentModel.DataAnnotations;
using WebShop.API.Helpers;

namespace WebShop.API.DTO.Requests
{
    public class UpdateUser
    {
        [Required]
        [StringLength(128, ErrorMessage = "Email must be less than 128 chars")]
        public string Email { get; set; }

        [StringLength(32, ErrorMessage = "Username must be less than 32 chars")]
        public string Password { get; set; }

        //Should only be possibel by admin user i
        //think this will make it possibel for anyone
        [Required]
        public Role Role { get; set; }
    }
}
