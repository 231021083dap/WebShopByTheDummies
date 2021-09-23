using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
