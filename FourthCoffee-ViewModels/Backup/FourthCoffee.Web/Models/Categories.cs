using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FourthCoffee.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Category")]
        [Required]
        public string Name { get; set; }

        [ReadOnly(true)]

        public virtual ICollection<Product> Products { get; set; }
    }
}