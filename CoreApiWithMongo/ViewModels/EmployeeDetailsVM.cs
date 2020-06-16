using CoreApiWithMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.ViewModels
{
    public class EmployeeDetailsVM :BaseViewModel
    {
        public Employee Employee { get; set; }
     
    }
}
