using WebShop.API.Helpers;

namespace WebShop.API.DTO.Requests
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }
}
