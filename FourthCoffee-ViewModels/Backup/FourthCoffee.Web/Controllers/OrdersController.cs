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
    public class OrdersController : Controller
    {
        private FourthCoffeeWebContext context = new FourthCoffeeWebContext();

        //
        // GET: /Orders/

        public ViewResult Index()
        {
            return View(context.Orders.Include(order => order.LineItems).ToList());
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(int id)
        {
            Order order = context.Orders.Single(x => x.Id == id);
            return View(order);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCustomers = context.Customers;
            return View();
        } 

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                context.Orders.Add(order);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleCustomers = context.Customers;
            return View(order);
        }
        
        //
        // GET: /Orders/Edit/5
 
        public ActionResult Edit(int id)
        {
            Order order = context.Orders.Single(x => x.Id == id);
            ViewBag.PossibleCustomers = context.Customers;
            return View(order);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleCustomers = context.Customers;
            return View(order);
        }

        //
        // GET: /Orders/Delete/5
 
        public ActionResult Delete(int id)
        {
            Order order = context.Orders.Single(x => x.Id == id);
            return View(order);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = context.Orders.Single(x => x.Id == id);
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}