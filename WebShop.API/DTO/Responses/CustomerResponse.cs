using System.Collections.Generic;

namespace WebShop.API.DTO.Responses
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public CustomerUserResponse User { get; set; }
        public List<CustomerAddressResponse> Addresses { get; set; } = new();
    }
}
