using HR.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HR.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: api/Employee
        public IEnumerable<Employee> Get()
        {
            using (var unitOfWork = new UnitOfWork(new HRContext())) {
                return unitOfWork.Employees.GetAll();
            }
        }

        // GET: api/Employee/5
        public Employee Get(int id)
        {
            using (var unitOfWork = new UnitOfWork(new HRContext()))
            {
                return unitOfWork.Employees.Get(id);
            }
        }

        // POST: api/Employee
        public void Post([FromBody]Employee employee)
        {
            using (var unitOfWork = new UnitOfWork(new HRContext()))
            {
                unitOfWork.Employees.AddEmployee(employee);
                unitOfWork.Complete();
            }
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]Employee employee)
        {
            using (var unitOfWork = new UnitOfWork(new HRContext()))
            {
                var originalEmployee = unitOfWork.Employees.Get(employee.Id);
                originalEmployee.FullName = employee.FullName;
                originalEmployee.HireDate = employee.HireDate;
                unitOfWork.Complete();
            }
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
            using (var unitOfWork = new UnitOfWork(new HRContext()))
            {
                unitOfWork.Employees.Remove(unitOfWork.Employees.Get(id));
                unitOfWork.Complete();
            }
        }
    }
}
