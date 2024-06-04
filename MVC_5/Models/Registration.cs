using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_5.Models
{
    public class Registration
    {
        
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public int age { get; set; }

        public string Section { get; set; }
    }
}