using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FourthCoffee.Models;

namespace FourthCoffee.Web.Controllers
{   
public class FeaturedController : Controller
{
    private FourthCoffeeWebContext context = new FourthCoffeeWebContext();

    // TempData sample
    public ViewResult Index()
    {
        // TempData is still available while the ViewBag and ViewData are not
        // To see  in action, uncomment code and set a breakpoint to investigate. You will see null values

        //var viewBagData = ViewBag.FeaturedProduct;
        //var tempData = TempData["Product"];

        var tempData = TempData["FeaturedProduct"];
        return View();            
    } 
}
}