using System;
using System.Collections.Generic;
using StoreApp.Library;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StoreApp.Web.Models
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }

        [Display(Name = "Location Name")]
        [Required, RegularExpression("[A-Z].*")]
        public string Name { get; set; }

        [Display(Name = "Location Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required, MaxLength(2)]
        public string State { get; set; }

        public List<Inventory> Inventory { get; set; }
    }
}
