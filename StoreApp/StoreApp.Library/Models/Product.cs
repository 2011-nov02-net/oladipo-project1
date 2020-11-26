using System;

namespace StoreApp.Library
{

    /// <summary>
    /// A product class, having a name, a product ID that increaments when creating a new instance of a product,
    /// the quantity of products and its availability. 
    ///</summary>

    public class Product
    {

        //product id
        public int ProductId { get; set; }
        //name
        private string _name;
        //price
        private decimal _price;

        //quantity in stock
        private int _quantity;
        public string Name
        {
            get { return _name; }
            //Name can not be an empty string. If name is epmty, throw an exception
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(message: "invalid Lastname", paramName: nameof(value));
                }
                _name = value;
            }

        }

        public decimal Price
        {
            get { return _price; }
            //the price of the product cannot be 0 or a negative number 
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Price can not be a negative");
                }
                _price = value;
            }
        }

        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Quantitiy can not be a negative");

                }
                _quantity = value;
            }
        }

        //constructor 

        public Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public Product(int id, string name, decimal price)
        {
            ProductId = id;
            Name = name;
            Price = price;
        }
        public Product(string name)
        {
            Name = name;
        }

        public void addQuantity(int amount)
        {
            Quantity = Quantity + amount;
        }

    }
}
