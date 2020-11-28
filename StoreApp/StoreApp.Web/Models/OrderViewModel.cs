using System;
using System.Collections.Generic;
using StoreApp.Library;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace StoreApp.Web.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Locations = new List<Location>();
            Customers = new List<Customer>();
        }

        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public List<Location> Locations { get; set; }
        public List<Customer> Customers { get; set; }

    }
}
