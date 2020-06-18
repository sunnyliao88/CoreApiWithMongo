using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.Data;
using CoreApiWithMongo.Models;
using CoreApiWithMongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiWithMongo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly DemoService _demoService;
       

        public HomeController(IEmployeeService employeeService, DemoService demoService)
        {
            _employeeService = employeeService;
            _demoService = demoService;
            
        }
        // GET: Home
        public string Index()
        {
           // _appDBContext.e
            return _demoService.DoSomething();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<Employee> employees = _employeeService.GetEmployees();
            //return new ObjectResult(employees);
            return View(employees); ;
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}