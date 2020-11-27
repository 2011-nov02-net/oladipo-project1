using System;
using System.Collections.Generic;
using System.Linq;
namespace StoreApp.Library
{
    public class Location
    {

        public int LocationId { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        public string State { get; set; }

        public List<Inventory> Inventory { get; set; }

        public Dictionary<string, int> Inventories { get; set; }

        private static int LocationIdSeed = 1;

        public Location()
        {
            Inventory = new List<Inventory>();
        }

        public Location(int id, string name, string address, string city, string state)
        {
            LocationId = id;
            Name = name;
            Address = address;
            City = city;
            State = state;

        }
        public Location(string city)
        {
            this.LocationId = LocationIdSeed;
            LocationIdSeed++;
            this.City = city;
            Inventories = new Dictionary<string, int>();

        }


    }


}