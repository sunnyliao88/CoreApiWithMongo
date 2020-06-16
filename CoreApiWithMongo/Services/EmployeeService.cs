using CoreApiWithMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        void Save(Employee employee);
    }

    public class MockEmployeeService : IEmployeeService
    {
        IEnumerable<Employee> _employees;
        public MockEmployeeService()
        {
            _employees = new List<Employee>
            {
                new Employee { ID=1,Name="name1",Department="IT", Email="name1@a.com"},
                new Employee { ID=2,Name="name2",Department="HR", Email="name2@a.com"},
                new Employee { ID=3,Name="name3",Department="HR", Email="name3@a.com"},
                new Employee { ID=4,Name="name4",Department="IT", Email="name4@a.com"},
                new Employee { ID=5,Name="name5",Department="HR", Email="name5@a.com"},
                new Employee { ID=6,Name="name6",Department="Account", Email="name6@a.com"},
                new Employee { ID=7,Name="name7",Department="HR", Email="name7@a.com"}

            };
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employees;
        }

        public void Save(Employee employee)
        {
            //will _employee save??
            Employee original = _employees.FirstOrDefault(e => e.ID == employee.ID);
            original = employee;

        }
    }

    public class MongoEmployeeService : IEmployeeService
    {
        IEnumerable<Employee> _employees;
        public MongoEmployeeService()
        {
            _employees = new List<Employee> {
                new Employee { ID=1,Name="name1",Department="IT", Email="name1@a.com"}
                , new Employee { ID=2,Name="name2",Department="HR", Email="name2@a.com"}
            };
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public void Save(Employee employee)
        {
            //will _employee save??
            Employee original = _employees.FirstOrDefault(e => e.ID == employee.ID);
            original = employee;

        }
    }

    public class SQLEmployeeService : IEmployeeService
    {
        IEnumerable<Employee> _employees;
        public SQLEmployeeService()
        {
            _employees = new List<Employee> {
                new Employee { ID=1,Name="name1",Department="IT", Email="name1@a.com"}
                , new Employee { ID=2,Name="name2",Department="HR", Email="name2@a.com"}
            };
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public void Save(Employee employee)
        {
            //will _employee save??
            Employee original = _employees.FirstOrDefault(e => e.ID == employee.ID);
            original = employee;

        }


    }

}
