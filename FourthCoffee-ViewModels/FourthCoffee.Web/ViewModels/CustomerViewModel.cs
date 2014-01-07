using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;
using FourthCoffee.Models;

namespace FourthCoffee.Web.ViewModels
{
public class CustomerViewModel 
{
    public Customer Customer { get; set; }
    public StatesDictionary States { get; set; }
    public CustomerViewModel(Customer customer)
    {
        Customer = customer;        
        States = new StatesDictionary();
    }
}
}