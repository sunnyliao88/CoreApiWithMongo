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

namespace CoreApiWithMongo.Services
{
    public interface IEmployeeService
    {
        Employee Add(Employee employee);
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);        
        Employee Update(Employee employee);
        Employee Delete(int id);
    }

    public class MockEmployeeService : IEmployeeService
    {
        private readonly IDepartmentService _departmentService;
        List<Employee> _employees;
       
        public MockEmployeeService(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            _employees = new List<Employee>
            {
                new Employee { ID=1,Name="name1", Email="name1@a.com",DepartmentId=1,Department= _departmentService.GetDepartments().FirstOrDefault(d=>d.Id==1)},
                new Employee { ID=2,Name="name2", Email="name2@a.com",DepartmentId=2,Department= _departmentService.GetDepartments().FirstOrDefault(d=>d.Id==2)},
                new Employee { ID=3,Name="name3", Email="name3@a.com",DepartmentId=1,Department= _departmentService.GetDepartments().FirstOrDefault(d=>d.Id==1)},
                new Employee { ID=4,Name="name4", Email="name4@a.com",DepartmentId=2,Department= _departmentService.GetDepartments().FirstOrDefault(d=>d.Id==2)}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.ID = _employees.Max(e => e.ID) + 1;            
            _employees.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.ID == id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
            return employee;
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employees;
        }

        public Employee Update(Employee model)
        {
            var employee = _employees.FirstOrDefault(e => e.ID == model.ID);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.DepartmentId = model.DepartmentId;

            }
            return employee;
        }

    }

    public class SQLEmployeeService : IEmployeeService
    {
        private readonly AppDBContext _appDBContext;

        public SQLEmployeeService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public Employee Add(Employee employee)
        {
            _appDBContext.Employees.Add(employee);
            _appDBContext.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _appDBContext.Employees.FirstOrDefault(e => e.ID == id);
            if (employee != null)
            {
                _appDBContext.Employees.Remove(employee);
                _appDBContext.SaveChanges();
            }
            return employee;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _appDBContext.Employees.Include(e => e.Department).FirstOrDefault(e => e.ID == id);
            return employee;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var employees = _appDBContext.Employees.Include(e => e.Department).ToList();
            return employees;
        }

        public Employee Update(Employee model)
        {
            Employee employee = _appDBContext.Employees.FirstOrDefault(e => e.ID == model.ID);
            if (employee != null)
            {
                EntityEntry<Employee> entry = _appDBContext.Employees.Attach(model);
                entry.State = EntityState.Modified;
                _appDBContext.SaveChanges();
            }
            return employee;
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
