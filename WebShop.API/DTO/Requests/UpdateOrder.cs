﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;

namespace WebShop.API.DTO.Requests
{
    public class UpdateOrder
    {
        [Required]
        public List<NewOrderItem> OrderItems { get; set; }

        [Required]
        public int ShipmentAddressId { get; set; }

        [Required]
        public int BillingAddressId { get; set; }
    }
}