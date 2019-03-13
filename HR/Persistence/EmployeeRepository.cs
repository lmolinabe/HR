using HR.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Persistence
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HRContext context) : base(context)
        {

        }

        public HRContext RepositoryContext
        {
            get { return Context as HRContext; }
        }

        public void AddEmployee(Employee employee)
        {
            if(employee.HireDate > DateTime.Now)
                return;

            RepositoryContext.Employees.Add(employee);
        }
    }
}