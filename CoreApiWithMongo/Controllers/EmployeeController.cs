using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.Models;
using CoreApiWithMongo.Services;
using CoreApiWithMongo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CoreApiWithMongo.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }



        [Route("/")]
        //   [Route("/[controller]")]
        [Route("")]
        //[Route("Employee")]
        //[Route("Employee/Index")]
        //[Route("Index")]
        //  [Route("[action]")]
        public ActionResult Index()
        {
            IEnumerable<Employee> employees = _employeeService.GetEmployees();
            //return new ObjectResult(employees);
            return View(employees);
        }


        // [Route("Details")]
        // [Route("Details/{id}")]
        //[Route("[action]/{id?}")]
        [Route("{id?}")]
        public ActionResult Details(int? id)
        {
            Employee employee = _employeeService.GetEmployeeById(id ?? 1);
            
            return View(employee);
            //return View("../test/test1", employee);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                Employee employee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    DepartmentId = model.DepartmentId
                };

                if (model.UploadFile != null)
                {
                    employee.FileName = model.UploadFile.FileName;
                    using (var ms = new MemoryStream())
                    {
                        model.UploadFile.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        string fileContent = Convert.ToBase64String(bytes);
                        employee.FileContent = fileContent;
                    }
                }

                Employee addedEmployee = _employeeService.Add(employee);
                return RedirectToAction(nameof(Details), new { id = addedEmployee.ID });
            }
            catch
            {
                return View(model);
            }
        }


        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            try
            {
                _employeeService.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employee);
            }
        }

        //  [Route("[action]")]
        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            try
            {
                _employeeService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employee);
            }
        }



        //[Route("{id?}")]
        //public ActionResult DisplayPdf(int? id)

        //{
        //    Employee employee = _employeeService.GetEmployeeById(id ?? 1);
        //    return new FileContentResult((employee.FileContent, "application/pdf");
            
        //}
    }
}