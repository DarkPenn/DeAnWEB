using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeAnWEB.Models.ViewModel
{
    public class ProfileVM
    {
        public string Username { get; set; }
        public string Role { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerID { get; set; }
    }

}