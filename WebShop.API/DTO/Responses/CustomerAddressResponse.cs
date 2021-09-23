namespace WebShop.API.DTO.Responses
{
    public class CustomerAddressResponse
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
        public string Floor { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
