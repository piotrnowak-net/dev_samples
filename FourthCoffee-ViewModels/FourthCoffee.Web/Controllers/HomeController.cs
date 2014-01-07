using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FourthCoffee.Models;

namespace FourthCoffee.Controllers
{
public class HomeController : Controller
{
// ViewBag & ViewData sample
public ActionResult Index()
{
    var featuredProduct = new Product
    {
        Name = "Special Cupcake Assortment!",
        Description = "Delectable vanilla and chocolate cupcakes",
        CreationDate = DateTime.Today,
        ExpirationDate = DateTime.Today.AddDays(7),
        ImageName = "cupcakes.jpg",
        Price = 5.99M,
        QtyOnHand = 12
    };

    
    ViewData["FeaturedProduct"] = featuredProduct;
    ViewBag.FeaturedProduct = featuredProduct;
    TempData["FeaturedProduct"] = featuredProduct;  

    return View();
}


        // TempData sample
        public ActionResult Featured()
        {
            var featuredProduct = new Product
            {
                Name = "Assorted Cupcakes",
                Description = "Delectable vanilla and chocolate cupcakes",
                CreationDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddDays(7),
                ImageName = "cupcakes.jpg",
                Price = 5.99M,
                QtyOnHand = 12
            };

            ViewData["FeaturedProduct"] = featuredProduct;
            ViewBag.FeaturedProduct = featuredProduct;
            TempData["FeaturedProduct"] = featuredProduct;

            //After the redirect, the ViewBag & ViewData objects are no longer available
            //Only TempData survives a redirect

            return new RedirectResult(@"~\Featured\");
        }


        public ActionResult About()
        {
            return View();
        }
    }
}
