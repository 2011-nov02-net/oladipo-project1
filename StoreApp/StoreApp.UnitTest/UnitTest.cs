using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using StoreApp.DataModel.Repositories;
using StoreApp.Web.Controllers;
using StoreApp.Web;
using StoreApp.Library;
using Moq;
using Xunit;

namespace StoreApp.UnitTest
{
    public class UnitTest
    {
        [Fact]
        public void AddCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Cristiano",
                LastName = "Ronaldo",
                Email = "CRonni@email.com"
            };

            Assert.True(customer.FirstName == "Cristiano" && customer.LastName == "Ronaldo" && customer.Email == "CRonni@email.com");
        }

        [Fact]
        public void AddLocation()
        {
            var location = new Location
            {
                Name = "Discount BestBuy",
                Address = "123 Road Rd",
                City = "City",
                State = "ST"
            };

            Assert.True(location.Name == "Discount BestBuy" && location.Address == "123 Road Rd" && location.City == "City" && location.State == "ST");
        }

        [Fact]
        public void AddProduct()
        {
            var product = new Product
            {
                Name = "Iphone",
                Price = 270
            };

            Assert.True(product.Name == "Iphone" && product.Price == 270);
        }


        [Fact]
        public void AddInventory()
        {
            var inventory = new Inventory
            {
                LocationId = 2,
                ProductId = 1,
                Quantity = 200
            };

            Assert.True(inventory.LocationId == 2 && inventory.ProductId == 1 && inventory.Quantity == 200);
        }
    }
}
