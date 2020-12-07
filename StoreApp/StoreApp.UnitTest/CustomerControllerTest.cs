using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using StoreApp.DataModel.Repositories;
using StoreApp.Web.Models;
using StoreApp.Web.Controllers;
using StoreApp.Web;
using StoreApp.Library;
using Moq;
using Xunit;
using StoreApplication.WebApp.Controllers;
using StoreApp.Library.Interfaces;

namespace StoreApp.UnitTest
{
    public class CustomerControllerTest
    {
        [Fact]
        public void Index_WithCustomers_DisplaysCustomers()
        {
            var mockRepo = new Mock<StoreAppRepository>();

            var Customers = new List<Customer>();

            var customer = new Customer()
            {
                CustomerId = 1,
                FirstName = "abc",
                LastName = "cdef",
                Email = "abc@email.com"
            };

            Customers.Add(customer);
            mockRepo.Setup(r => r.GetCustomers())
                .Returns(Customers);

           var controller = new CustomerController(mockRepo.Object, new NullLogger<CustomerController>());

            IActionResult actionResult = controller.Index(null);

            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var customers = Assert.IsAssignableFrom<IEnumerable<DataModel.CustomerViewModel>>(viewResult.Model);
            var customerList = customers.ToList();
            Assert.Single(customerList);
            Assert.Equal("abc", customerList[0].FirstName);
            Assert.Equal("cdef", customerList[0].LastName);
            Assert.Equal("abc@email.com", customerList[0].Email);
    
        }
    }
}
