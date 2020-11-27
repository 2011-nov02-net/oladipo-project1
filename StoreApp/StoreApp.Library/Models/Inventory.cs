using System;
using System.Collections.Generic;
using System.Linq;
namespace StoreApp.Library
{
    public class Inventory
    {
    public Inventory(int locationId, int productId, int quantity)
        {

        LocationId = locationId;
        ProductId = productId;
        Quantity = quantity;
        }

        public Inventory() { }


        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }

}