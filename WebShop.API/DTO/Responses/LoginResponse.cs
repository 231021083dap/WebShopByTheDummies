using WebShop.API.Helpers;

namespace WebShop.API.DTO.Responses
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}
