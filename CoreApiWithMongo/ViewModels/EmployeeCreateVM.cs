﻿using CoreApiWithMongo.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.ViewModels
{
    public class EmployeeCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DepartmentEnum? Department { get; set; }

        public int DepartmentId { get; set; }
        public IFormFile UploadFile { get; set; }

    }


    
}
