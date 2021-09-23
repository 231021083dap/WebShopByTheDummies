using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrdreDate { get; set; }

        [ForeignKey("Address.Id")]
        public int AddressId { get; set; }

        [Required]
        public List<OrderItem> OrderItems { get; set; }

        public Address address { get; set; }
    }
}
