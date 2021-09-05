using BankSystem.Models;
using BankSystem.Services;
using System.Collections.Generic;
using Xunit;
using System;
using System.Threading;

namespace BankSystem.Tests
{
    public class BankServicesTest
    {
        [Fact]
        public void AddAndFindClientTest()
        {
            //Arrange
            var clients = new BankServices<Client>() { clientList = new List<Client>() };
            var newClient = new Client() { PassportId = "9999999", BirthDate = new System.DateTime(1999, 09, 09), Surname = "Brown", Name = "Ben" };

            //Act
            clients.Add(newClient);
            var result1 = clients.FindClient(newClient);

            //Assert
            Assert.Contains<IPerson>(newClient, clients.clientList);
            Assert.Equal(newClient, result1);
        }

        [Fact]
        public void AddAndFindEmployeeTest()
        {
            //Arrange
            var employees = new BankServices<Employee>() { employeeList = new List<Employee>() };
            var newEmployee = new Employee() { PassportId = "8888888", BirthDate = new System.DateTime(1988, 08, 08), Surname = "Green", Name = "Gerbert" };

            //Act
            employees.Add(newEmployee);
            var result2 = employees.FindEmployee(newEmployee);

            //Assert
            Assert.Contains<IPerson>(newEmployee, employees.employeeList);
            Assert.Equal(newEmployee, result2);
        }

        //параллельное начисление денег на счет одного 
        //и того же клиента из двух разных потоков
        [Fact]
        public void MoneyTransferTest_Parallel_Transaction()
        {
            //Arrange
            var donorAccount1 = new Account() { CurrencyType = new Rub(), CurrencyAmount = 300 };
            var donorAccount2 = new Account() { CurrencyType = new Rub(), CurrencyAmount = 500 };
            var recipientAccount = new Account() { CurrencyType = new Rub(), CurrencyAmount = 100 };

            var bankService = new BankServices<Client>();
            var exchange = new Exchange();
            object locker = new object();

            //Act
            ThreadPool.QueueUserWorkItem(_ =>
            {
                lock (locker)
                {
                    bankService.MoneyTransfer(300, donorAccount1, recipientAccount, exchange.CurrencyExchange);
                }
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                lock (locker)
                {
                    bankService.MoneyTransfer(500, donorAccount2, recipientAccount, exchange.CurrencyExchange);
                }
            });
            Console.WriteLine(recipientAccount.CurrencyAmount);

            //Assert
            Assert.Equal(900, recipientAccount.CurrencyAmount);
            Assert.Equal(0, donorAccount1.CurrencyAmount);
            Assert.Equal(0, donorAccount2.CurrencyAmount);
        }
    }
}