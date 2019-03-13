using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Persistence
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void AddEmployee(Employee employee);
    }
}
