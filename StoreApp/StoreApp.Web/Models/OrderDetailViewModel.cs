using System;
using System.Collections.Generic;
using StoreApp.Library;
using System.Linq;
using System.Threading.Tasks;
namespace StoreApp.Web.Models
{
    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {
            Products = new List<Product>();
        }
        public OrderDetailViewModel(int orderId, int productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public List<Product> Products { get; set; }
    }
}
