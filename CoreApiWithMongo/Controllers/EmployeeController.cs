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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;

namespace CoreApiWithMongo.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
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
            EmployeeCreateVM model = new EmployeeCreateVM();
            var selectList = new List<SelectListItem>();
            selectList.Add(
                new SelectListItem()
                {
                    Text = "Select..",
                    Value = ""
                });

            foreach (var department in _departmentService.GetDepartments())
            {
                selectList.Add(new SelectListItem() { Text = department.DepartmentName, Value = department.Id.ToString() });
            }
            model.Departments = selectList;
            return View(model);
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

        [Route("{id}")]
        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            var selectList = new List<SelectListItem>();

            foreach (var department in _departmentService.GetDepartments())
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.DepartmentName,
                    Value = department.Id.ToString()
                };                
                selectList.Add(selectListItem);
            }


            EmployeeEditVM model = new EmployeeEditVM()
            {
                Departments = selectList,
                
                Name = employee.Name,
                Email = employee.Email,  
                DepartmentId=employee.DepartmentId                
            };
            return View(model);
        }

        [Route("{id}")]
        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                Employee employee = _employeeService.GetEmployeeById(id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.DepartmentId = model.DepartmentId;

                _employeeService.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        [Route("{id}")]
        [HttpGet]
        //  [Route("[action]")]
        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                return View(employee);
            }
            else
            {
                 return new NotFoundResult();
            }
        }

        // POST: Employee/Delete/5
        [Route("{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                employee = _employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return new NotFoundResult();
                }
                _employeeService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                  return StatusCode(500);
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