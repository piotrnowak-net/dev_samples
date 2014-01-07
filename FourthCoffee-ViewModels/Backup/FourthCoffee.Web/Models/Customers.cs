using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FourthCoffee.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]        
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [RegularExpression(@"^((\d{5}-\d{4})|(\d{5})|([A-Z]\d[A-Z]\s\d[A-Z]\d))$",
                           ErrorMessage="The Postal code must be in a correct format; Zip or Zip+4")]
        [Required]
        public string PostalCode { get; set; }
        
        [RegularExpression(@"^([\w\-\.]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$",
                            ErrorMessage="The email address must be in the correct format; name@example.com")]
        [Required]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}