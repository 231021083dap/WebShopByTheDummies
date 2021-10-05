using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.API.Database.Entities
{
    public class ZipCity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        //public List<Address> Addresses { get; set; }
    }
}
