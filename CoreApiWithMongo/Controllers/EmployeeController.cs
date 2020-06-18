using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.Models;
using CoreApiWithMongo.Services;
using CoreApiWithMongo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            EmployeeDetailsVM model = new EmployeeDetailsVM();
            model.Employee = employee;
            return View(model);
            //return View("../test/test1", employee);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                Employee addedEmployee = _employeeService.Add(employee);
                return RedirectToAction(nameof(Details), new { id = addedEmployee.ID });
            }
            catch
            {
                return View(employee);
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
    }
}