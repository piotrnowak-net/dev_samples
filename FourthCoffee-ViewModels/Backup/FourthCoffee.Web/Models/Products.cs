using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FourthCoffee.Models
{
    public class Product 
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [DisplayName("Delicious Treat")]
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "The product description field is required.")]
        public string Description { get; set; }

        [DisplayName("Sale Price")]
        [Required]
        public decimal Price { get; set; }

        [DisplayName("Made fresh on")]
        [Required]
        public DateTime CreationDate { get; set; }
        
        [DisplayName("Don't Sell After")]
        [Required]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Qty Available")]
        [Required]
        [Range(0, 120, ErrorMessage = "The Qty Available must be between 0 and 120.")]
        public int QtyOnHand { get; set; }

        [DisplayName("Product Image")]
        public string ImageName { get; set; }
    }
}




