using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FourthCoffee.Models;
using FourthCoffee.Web.ViewModels;

namespace FourthCoffee.Models
{
public class FourthCoffeeWebContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLineItem> OrderLineItems { get; set; }     
}
  

    // builds bakery.sdf database and adds seed data
public class FourthCoffeeWebContextContextInitializer : 
    DropCreateDatabaseAlways<FourthCoffeeWebContext>
{        
    protected override void Seed(FourthCoffeeWebContext context)
    {

                new List<Customer> {
            new Customer { FirstName = "Rachel", LastName = "Appel", Address = "Microsoft 1290 6th Ave", 
                            City = "NYC", State = "NY", PostalCode = "10016", Email = "nospam@example.com", 
                            Orders = new List<Order> { 
                            new Order { CustomerId=1,  Date=DateTime.Now,  ReadyDateTime=DateTime.Now, OrderType=OrderType.Pickup, Total=12.99M }},
            }}.ForEach(c => context.Customers.Add(c));;
        
        new List<Category> {
            new Category() 
            { 
                Name="Cakes & Cupcakes",
                Products = new List<Product> {
                        new Product 
                        { Name="Assorted Cupcakes", Description="Delectable vanilla and chocolate cupcakes", 
                            CreationDate=DateTime.Today, ExpirationDate=DateTime.Today.AddDays(7), 
                            ImageName="cupcakes.jpg", Price=5.99M, QtyOnHand=12},
                        new Product 
                        { Name="Chocolate Cake", Description="Rich chocolate frosting cover this chocolate lover’s dream", 
                            CreationDate=DateTime.Today, ExpirationDate=DateTime.Today.AddDays(7), 
                            ImageName="chocolate_cake.jpg", Price=9.99M, QtyOnHand=12},
                        new Product 
                        { Name="Carrot Cake", Description="scrumptious mini-carrot cake encrusted with sliced almonds", 
                            CreationDate=DateTime.Today, ExpirationDate=DateTime.Today.AddDays(7), 
                            ImageName="carrot_cake.png", Price=8.99M, QtyOnHand=12}
                },                                                                  
            },                        
            new Category() 
            { 
                Name="Pies & Tarts",
                Products = new List<Product> {
                        new Product 
                        { Name="Lemon Tart", Description="A delicious lemon tart with fresh meringue cooked to perfection", 
                            CreationDate=DateTime.Today, ExpirationDate=DateTime.Today.AddDays(7), 
                            ImageName="lemon_tart.jpg", Price=10.99M, QtyOnHand=12},
                        new Product 
                        { Name="Pear Tart", Description="A glazed pear tart topped with sliced almonds and a dash of cinnamon", 
                            CreationDate=DateTime.Today, ExpirationDate=DateTime.Today.AddDays(7), 
                            ImageName="pear_tart.jpg", Price=8.99M, QtyOnHand=12}       
                },                                                                  
            }
        }.ForEach(c => context.Categories.Add(c));

        base.Seed(context);
    }
    //private seedCustomer(){
    //        new List<Customer> {
    //    new Customer { FirstName = "Rachel", LastName = "Appel", Address = "Microsoft 1290 6th Ave", 
    //                    City = "NYC", State = "NY", PostalCode = "10016", Email = "nospam@example.com", 
    //                    Orders = new List<Order> { 
    //                    new Order { CustomerId=1,  Date=DateTime.Now,  ReadyDateTime=DateTime.Now, OrderType=OrderType.Pickup, Total=12.99M }},
    //    }}.ForEach(c => context.Customers.Add(c));;
    //}
}

}