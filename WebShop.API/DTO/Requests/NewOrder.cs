using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTO.Requests
{
    public class NewOrder
    {
        [Required]
        public List<NewOrderItem> OrderItems { get; set; }

        [Required]
        public int ShipmentAddressId { get; set; }

        [Required]
        public int BillingAddressId { get; set; }
    }
}
