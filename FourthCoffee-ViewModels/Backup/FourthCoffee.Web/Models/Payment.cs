
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;
using FourthCoffee.Models;

namespace FourthCoffee.Models
{

    public enum PaymentType
    {
        Cash,
        Debit,
        Visa,
        MasterCard
    }

   

    public class Payment
    {
        private bool _approved = false;
        public bool Approved 
        {
             get  
             {
                return true;
             }
        }

        public bool Process(Order order)
        {

            //traditional way to process an order
            switch (order.PaymentType)
            {
                case PaymentType.Cash:
                    {
                        //Accept cash
                        _approved = true;
                        break;
                    }
                case PaymentType.Debit:
                    {
                        //Process debit card
                        _approved = true;
                        break;
                    }
                case PaymentType.MasterCard:
                    {
                        //Process MC credit card
                        _approved = true;
                        break;
                    }
                case PaymentType.Visa:
                    {
                        //Process Visa credit card
                        _approved = true;
                        break;
                    }
                default:
                    {
                        _approved = false;
                        break;
                    }                
            }
            return _approved;
        }
    }        
}