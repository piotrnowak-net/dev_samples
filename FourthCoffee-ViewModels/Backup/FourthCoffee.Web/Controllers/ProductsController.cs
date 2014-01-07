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
    public class ProductsController : Controller
    {
        private FourthCoffeeWebContext context = new FourthCoffeeWebContext();

        //
        // GET: /Products/

        public ViewResult Index()
        {
            return View(context.Products.Include(product => product.Category).ToList());
        }

        public ViewResult SomethingElse()
        {
            return View(context.Products.Include(product => product.Category).ToList());
        }

        
        
        //
        // GET: /Products/Details/5

        public ViewResult Details(int id)
        {
            Product product = context.Products.Single(x => x.Id == id);
            return View(product);
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCategories = context.Categories;
            return View();
        } 

        //
        // POST: /Products/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleCategories = context.Categories;
            return View(product);
        }
        
        //
        // GET: /Products/Edit/5
 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = context.Products.Single(x => x.Id == id);
            ViewBag.PossibleCategories = context.Categories;
            return View(product);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleCategories = context.Categories;
            return View(product);
        }

        //
        // GET: /Products/Delete/5
 
        public ActionResult Delete(int id)
        {
            Product product = context.Products.Single(x => x.Id == id);
            return View(product);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = context.Products.Single(x => x.Id == id);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}