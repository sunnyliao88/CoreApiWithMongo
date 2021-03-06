﻿using CoreApiWithMongo.Enums;
using CoreApiWithMongo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public int DepartmentId { get; set; }
        public IFormFile UploadResume { get; set; }
        public IFormFile UploadPhoto { get; set; }

    }


    
}
