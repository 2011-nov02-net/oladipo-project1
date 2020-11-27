using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataModel
{
    public partial class Inventory
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }

        public Inventory(int locationId, int productId, int quantity)
        {

            LocationId = locationId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Inventory() { }
    }
}
