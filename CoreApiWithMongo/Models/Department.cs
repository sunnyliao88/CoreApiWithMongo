using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Models
{
    public class Department
    {
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual IEnumerable<Employee> Employees { get; set; }

    }
}
