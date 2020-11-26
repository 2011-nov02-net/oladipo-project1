using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp.Library
{

    public class Customer
    {

        //name
        private string _firstName;
        private string _lastName;

        public string Email { get; set; }
        //customer id
        public int CustomerId { get; set; }
        //default store 
        public int DefaultStore { get; set; }

        //list of all orders
        public List<Product> CustomerProducts { get; set; }


        // set names to not allow empty string
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(message: "invalid FIrstname", paramName: nameof(value));
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(message: "invalid Lastname", paramName: nameof(value));
                }
                _lastName = value;
            }
        }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        //constructor

        private static int CustomerIdSeed = 1;

        public Customer()
        {
            this.CustomerId = CustomerIdSeed;
            CustomerIdSeed++;
            CustomerProducts = new List<Product>();
        }

        public Customer(int CustomerId)
        {
            this.CustomerId = CustomerId;
            CustomerProducts = new List<Product>();
        }
        public Customer(string first, string last, string email)
        {
            FirstName = first;
            LastName = last;
            Email = email;
        }

        public Customer(int id, string first, string last, string email)
        {

            CustomerId = id;
            FirstName = first;
            LastName = last;
            Email = email;

        }


    }
}