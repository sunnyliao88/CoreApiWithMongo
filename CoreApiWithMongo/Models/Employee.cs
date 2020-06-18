using CoreApiWithMongo.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Models
{
    public class Employee
    {        
        public int ID { get; set; }

        [Required]
        public string  Name { get; set; }

        [Required]
        [EmailAddress]
        public string  Email { get; set; }

        [Required]      
        public DepartmentEnum? Department { get; set; }

        public int DepartmentId { get; set; }
        
    }
}
