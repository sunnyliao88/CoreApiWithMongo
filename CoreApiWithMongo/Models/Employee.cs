using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Models
{
    public class Employee
    {
        [Required]
        public int ID { get; set; }
        
        public string  Name { get; set; }

        [EmailAddress]
        public string  Email { get; set; }
        public string Department { get; set; }
    }
}
