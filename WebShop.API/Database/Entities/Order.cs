using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        //Date maybe needs to be string to allow separator labels
        [Required]
        public int OrdreDate { get; set; }

        [ForeignKey("Address.Id")]
        public int AddressId { get; set; }
    }
}
