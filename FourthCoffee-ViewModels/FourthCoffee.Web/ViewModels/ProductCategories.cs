using System.Collections.Generic;
using System.Web.Mvc;
using FourthCoffee.Models;
using System.Collections;
namespace FourthCoffee.Web.ViewModels
{
    public class ProductCategoriesViewModel
    {
        public Product Product { get; set; }
        public SelectList Categories { get; set; }
        private FourthCoffeeWebContext context = new FourthCoffeeWebContext();

        public ProductCategoriesViewModel(Product product)
        {
            Product = product;            
        }
    }    

}