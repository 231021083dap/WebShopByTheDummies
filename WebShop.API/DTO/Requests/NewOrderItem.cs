using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class NewOrderItem
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CurrentPrice { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
