namespace WebShop.API.DTO.Responses
{
    public class CustomerResponse
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public CustomerUserResponse User { get; set; }

        //public CustomerAddressResponse Addresses { get; set; }

        public class CustomerUserResponse
        {
            public int Id { get; set; }
            public string Email { get; set; }
        }

        //public class CustomerAddressResponse
        //{
        //    public int Id { get; set; }
        //    public string StreetName { get; set; }
        //    public int Number { get; set; }
        //    public string Floor { get; set; }
        //    public AddressZipCityResponse ZipCity { get; set; }
        //    public string County { get; set; }
        //}

        //public class AddressZipCityResponse
        //{
        //    public int Zipcode { get; set; }
        //    public string City { get; set; }
        //}
    }
}
