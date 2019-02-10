﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.ClientCert.Demo.Models;

namespace WebApi.ClientCert.NetCore.Controllers
{
    [Produces("application/json")]
    [Route("Customer")]    
    public class CustomerController : Controller
    {
        // GET: api/Customer               
        public IList<Customer> Get()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer() { Name = "Nice customer", Address = "USA", Telephone = "123345456" },
                new Customer() { Name = "Good customer", Address = "UK", Telephone = "9878757654" },
                new Customer() { Name = "Awesome customer", Address = "France", Telephone = "34546456" }
            };

            return customers;
        }        
    }
}
