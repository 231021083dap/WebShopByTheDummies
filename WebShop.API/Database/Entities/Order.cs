using System;
using System.Collections.Generic;
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
        public DateTime OrderDate { get; set; }

        [ForeignKey("Address.Id")]
        public int ShipmentAddressId { get; set; }

        [ForeignKey("Address.Id")]
        public int BillingAddressId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();

        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
    } 
}
