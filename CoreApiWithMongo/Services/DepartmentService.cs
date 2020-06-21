using CoreApiWithMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.Enums;
using CoreApiWithMongo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CoreApiWithMongo.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreApiWithMongo.Services
{
    public interface IDepartmentService
    {       
        IEnumerable<Department> GetDepartments();
        List<SelectListItem> GetDepartmentSelectListItems();
    }

    public class MockDepartmentService : IDepartmentService
    {        
        List<Department> _departments;
        public MockDepartmentService()
        {
            _departments = new List<Department>() {
                new Department() { Id = 1, DepartmentName = "IT" },
                new Department() { Id = 2, DepartmentName = "HR" }
            };           
        }        

        public IEnumerable<Department> GetDepartments()
        {
            return _departments;
        }

        public List<SelectListItem> GetDepartmentSelectListItems()
        {
            throw new NotImplementedException();
        }
    }


    public class SQLDepartmentService : IDepartmentService
    {
        private readonly AppDBContext _appDBContext;

        public SQLDepartmentService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IEnumerable<Department> GetDepartments()
        {
            var departments = _appDBContext.Departments;
            return departments;
        }

        public List<SelectListItem> GetDepartmentSelectListItems()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(
                new SelectListItem()
                {
                    Text = "Select..",
                    Value = ""
                });
            foreach (var department in _appDBContext.Departments)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.DepartmentName,
                    Value = department.Id.ToString()
                };
                result.Add(selectListItem);
            }
            return result;
        }


    }

    /*
     public class MongoEmployeeService : IEmployeeService
    {
        List<Employee> _employees;
        public MongoEmployeeService()
        {
            _employees = new List<Employee> {
                new Employee { ID=1,Name="name1",Department=DepartmentEnum.IT, Email="name1@a.com"},
                new Employee { ID=2,Name="name2",Department=DepartmentEnum.HR, Email="name2@a.com"},
                new Employee { ID=3,Name="name3",Department=DepartmentEnum.HR, Email="name3@a.com"},
                new Employee { ID=4,Name="name4",Department=DepartmentEnum.IT, Email="name4@a.com"},
                new Employee { ID=5,Name="name5",Department=DepartmentEnum.HR, Email="name5@a.com"},
                new Employee { ID=6,Name="name6",Department=DepartmentEnum.Account, Email="name6@a.com"},
                new Employee { ID=7,Name="name7",Department=DepartmentEnum.HR, Email="name7@a.com"}
            };
        }

        public Employee Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee Delete(int id)
        {
            Employee employee = _employees.FirstOrDefault(e => e.ID == id);
            _employees.Remove(employee);
            return employee;            
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }
     

        public Employee Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
    */



}
