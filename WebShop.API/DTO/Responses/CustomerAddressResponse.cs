namespace WebShop.API.DTO.Responses
{
    public class CustomerAddressResponse
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
        public string Floor { get; set; }
        public ZipCityResponse ZipCity { get; set; }
        public string County { get; set; }
    }
}
