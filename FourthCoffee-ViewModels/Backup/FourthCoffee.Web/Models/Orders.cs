using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;
using FourthCoffee.Models;

namespace FourthCoffee.Models
{
    public class Order
    {
        public int Id { get; set; }                
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime  Date { get; set; }        
        public decimal   Total { get; set; }
        [Required]
        public DateTime  ReadyDateTime { get; set; }
        [Required]
        [DefaultValue("Pickup")]
        public OrderType OrderType { get; set; }

        public ICollection<OrderLineItem> LineItems { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public class OrderLineItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }


    public enum OrderType
    {        
        Pickup=1,
        Delivery=2,
        Special=3
    }

}