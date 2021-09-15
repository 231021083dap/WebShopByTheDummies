namespace WebShop.API.DTO.Responses
{
    public class AddressResponse
    {
        public CustomerResponse CustomerId { get; set; }
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
        public string Floor { get; set; }
        public ZipCityResponse Zipcode { get; set; }
        public string Country { get; set; }
    }
}
