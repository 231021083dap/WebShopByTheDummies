namespace WebShop.API.DTO.Responses
{
    public class AddressResponse
    {
        public AddresseCustomerResponse AddressCustomer { get; set; }
        public CustomerResponse Customer { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
        public string Floor { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
    public class AddresseCustomerResponse
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public CustomerUserResponse User { get; set; }
    }
}
