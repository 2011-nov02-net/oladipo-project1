using System;

namespace StoreApp.Library
{
    public class OrderDetail
    {
    public OrderDetail()
    {
         
    }

     public OrderDetail(int orderId, int productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

}
