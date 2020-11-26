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

        public Dictionary<string, int> Inventories { get; set; }

        private static int LocationIdSeed = 1;

        public Location() { }

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

        //add inventory
        public void addInventory(string name, int quantity)
        {
            if (Inventories.ContainsKey(name))
            {
                Inventories[name] += quantity;
            }
            else
            {
                Inventories.Add(name, quantity);
            }

        }
        //remove inventory
        public void reduceInventory(string name, int quantity)
        {
            Inventories[name] -= quantity;
        }

        public void deleteItemsFromInventory(string name)
        {
            Inventories.Remove(name);
        }

        public List<string> getInventory()
        {
            var inventory = new List<string>();
            int index = 1;

            foreach (KeyValuePair<string, int> product in Inventories)
            {
                string value = $"{index.ToString()}\t{product.Key}\t{product.Value.ToString()}";
                inventory.Add(value);
                index++;
            }

            return inventory;
        }

    }


}