using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FourthCoffee.Models;
using FourthCoffee.Web.ViewModels;

namespace FourthCoffee.Web.Controllers
{   
    public class CustomersController : Controller
    {
        private FourthCoffeeWebContext context = new FourthCoffeeWebContext();

        //
        // GET: /Customers/

        public ViewResult Index()
        {
            return View(context.Customers.Include(customer => customer.Orders).ToList());
        }

        public ActionResult Edit(int id)
        {
            Customer customer = context.Customers.Single(x => x.Id == id);
            CustomerViewModel customerViewModel = new CustomerViewModel(customer);
            return View(customerViewModel);
        }




        public ViewResult Details(int id)
        {
            Customer customer = context.Customers.Single(x => x.Id == id);
            return View(customer);
        }

        //
        // GET: /Customers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Customers/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(customer);
        }
        


        //
        // GET: /Customers/Delete/5
 
        public ActionResult Delete(int id)
        {
            Customer customer = context.Customers.Single(x => x.Id == id);
            return View(customer);
        }

        //
        // POST: /Customers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = context.Customers.Single(x => x.Id == id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}