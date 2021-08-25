using Bank_Sistem.Services;
using BankSystem.Models;
using BankSystem.Services;
using System.Collections.Generic;
using Xunit;

namespace BankSystem.Tests
{
    public class BankServicesTest
    {
        [Fact]
        public void AddAndFindPersonTest()
        {
            //Arrange
            var clients = new BankServices<Client>() { clientList = new List<Client>() };
            var employees = new BankServices<Employee>() { employeeList = new List<Employee>() };
            var newClient = new Client() { PassportId = "9999999", BirthDate = new System.DateTime(1999, 09, 09), Surname = "Brown", Name = "Ben" };
            var newEmployee = new Employee() { PassportId = "8888888", BirthDate = new System.DateTime(1988, 08, 08), Surname = "Green", Name = "Gerbert" };

            //Act
            clients.Add(newClient);
            employees.Add(newEmployee);
            var result1 = clients.FindClient(newClient);
            var result2 = employees.FindEmployee(newEmployee);

            //Assert
            Assert.Contains<IPerson>(newClient, clients.clientList);
            Assert.Contains<IPerson>(newEmployee, employees.employeeList);
            Assert.Equal(newClient, result1);
            Assert.Equal(newEmployee, result2);
        }
    }
}