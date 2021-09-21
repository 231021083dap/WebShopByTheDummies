using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;

namespace WebShop.API.DTO.Requests
{
    public class NewOrder
    {
           
        [Required]
        public List<NewOrderItem> OrderItems { get; set; }

        [Required]
        public NewAddress ShipmentAddress { get; set; }

        [Required]
        public NewAddress BillingAddress { get; set; }
    }

}

