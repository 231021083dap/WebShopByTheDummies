namespace WebShop.API.DTO.Responses
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public UserResponse UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
