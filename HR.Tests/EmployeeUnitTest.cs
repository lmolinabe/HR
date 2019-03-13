using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HR.Persistence;
using Moq;

namespace HR.Tests
{
    [TestClass]
    public class EmployeeUnitTest
    {
        [TestMethod]
        public void AddEmployeeHireDateFailedTest()
        {
            var employeeStub = new Employee();
            employeeStub.Id = 1;
            employeeStub.FullName = "Test Employee";
            employeeStub.HireDate = DateTime.Now.AddDays(2).Date;

            var hrContextMock = new Moq.Mock<HRContext>();
            var failed = true;

            using (var unitOfWork = new UnitOfWork(hrContextMock.Object))
            {
                unitOfWork.Employees.AddEmployee(employeeStub);
                unitOfWork.Complete();
            }

            try{
                hrContextMock.Verify(mock => mock.Employees.Add(new Employee()), Times.Once());
                failed = false;
            }
            catch {
                failed = true;
            }

            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void AddEmployeeHireDatePassedTest()
        {
            var employeeStub = new Employee();
            employeeStub.Id = 1;
            employeeStub.FullName = "Test Employee";
            employeeStub.HireDate = DateTime.Now;

            var hrContextMock = new Moq.Mock<HRContext>();
            hrContextMock.Setup(x => x.Employees.Add(employeeStub));
            hrContextMock.Setup(x => x.SaveChanges()).Returns(1);

            var passed = true;

            using (var unitOfWork = new UnitOfWork(hrContextMock.Object))
            {
                unitOfWork.Employees.AddEmployee(employeeStub);
                unitOfWork.Complete();
            }

            try
            {
                hrContextMock.Verify(mock => mock.Employees.Add(employeeStub), Times.Once());
            }
            catch
            {
                passed = false;
            }

            Assert.IsTrue(passed);
        }
    }
}
