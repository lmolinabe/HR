using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRContext _context;

        public UnitOfWork(HRContext context) {
            _context = context;
            Employees = new EmployeeRepository(_context);
        }

        public IEmployeeRepository Employees { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}