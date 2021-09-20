using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.Database.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order.Id")]
        public int OrderId { get; set; }

        [ForeignKey("Product.Id")]
        public int ProductId { get; set; }

        [Required]
        public int Amount { get; set; }

        //Needs to be a get of some kind?
        //How would it change the price else? 
        [Required]
        public int CurrentPrice { get; set; }
    }
}
