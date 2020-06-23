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
using CoreApiWithMongo.Extensions;
using AutoMapper;
using static MongoDB.Libmongocrypt.CryptContext;
using System.Net;

namespace CoreApiWithMongo.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService,
            IDepartmentService departmentService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapper = mapper;
        }



        [Route("/")]
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
            if (employee == null)
            {
                return  NotFound();
            }
            return View(employee);
            //return View("../test/test1", employee);
        }


        public ActionResult Create()
        {
            EmployeeCreateVM model = new EmployeeCreateVM();
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

            Employee employee = _mapper.Map<Employee>(model);
            if (string.IsNullOrEmpty(employee.Photo))
            {
                employee.Photo = _employeeService.GetDefaultPhoto();
            }

            Employee addedEmployee = _employeeService.Add(employee);
            return RedirectToAction(nameof(Details), new { id = addedEmployee.ID });
        }

        [Route("{id}")]
        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return  NotFound();
            }
            EmployeeEditVM model = _mapper.Map<EmployeeEditVM>(employee);
            return View(model);
        }

        [Route("{id}")]
        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeEditVM model)
        {
            ModelState.Remove("Name");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Employee newEmployee = _mapper.Map<Employee>(model);
            Employee employee = _employeeService.GetEmployeeById(id);
            employee.Email = newEmployee.Email;
            employee.DepartmentId = newEmployee.DepartmentId;
            employee.Photo = newEmployee.Photo ?? employee.Photo;
            employee.Resume = newEmployee.Resume ?? employee.Resume;

            _employeeService.Update(employee);
            return RedirectToAction(nameof(Index));
        }

        [Route("{id}")]
        [HttpGet]
        //  [Route("[action]")]
        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return  NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [Route("{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            _employeeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }





        //[Route("{id?}")]
        //public ActionResult DisplayPdf(int? id)

        //{
        //    Employee employee = _employeeService.GetEmployeeById(id ?? 1);
        //    return new FileContentResult((employee.FileContent, "application/pdf");

        //}
    }
}