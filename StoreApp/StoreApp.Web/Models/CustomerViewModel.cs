using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace StoreApp.DataModel
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

      
        [Required]
        public string Email { get; set; }
    }
}
