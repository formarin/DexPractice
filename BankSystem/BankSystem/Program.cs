using Bank_Sistem.Services;
using BankSystem.Models;
using BankSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = new BankServices<Employee> { employeeList = new List<Employee>() };
            var clients = new BankServices<Client> { clientList = new List<Client>() };
            var fakeDataService = new FakeDataService();

            ////заполнение коллекций
            //while (clients.clientList.Count <= 10000)
            //{
            //    var newClient = fakeDataService.GenerateClient();
            //    clients.Add(newClient);
            //}
            //while (employees.employeeList.Count <= 1000)
            //{
            //    var newEmployee = fakeDataService.GenerateEmployee();
            //    employees.Add(newEmployee);
            //}

            ////заполнение словаря
            //var client1 = fakeDataService.GenerateClient();
            //var client2 = fakeDataService.GenerateClient();

            //var account1 = new Account { CurrencyType = new Rup(), CurrencyAmount = 10000 };
            //var account2 = new Account { CurrencyType = new Eur(), CurrencyAmount = 15 };
            //var account3 = new Account { CurrencyType = new Mdl(), CurrencyAmount = 1000 };

            //var dict = new Dictionary<string, List<Account>>
            //{
            //    { client1.PassportId, new List<Account> { account1, account2 } },
            //    { client2.PassportId, new List<Account> { account3 } }
            //};

            ////словарь в файл и обратно
            //var dictPath = Path.Combine("C:", "Users", "Irina", "source", "repos", "DexPractice",
            //    "BankSystem", "BankSystem", "FileFolder", "ClientsAndAccountsDictionary.txt");

            //clients.AddDictionaryToFile(dict, dictPath);
            //var desDict = clients.GetDictionaryFromFile(dictPath);

            //вывод свойств и значений объекта
            //var employee = fakeDataService.GenerateEmployee();
            //var client = fakeDataService.GenerateClient();
            //var account = new Account() { CurrencyType = new Rup(), CurrencyAmount = 10 };

            //var expData = new ExportData();
            //expData.AddDataToFile<Employee>(employee);
            //expData.AddDataToFile<Client>(client);
            //expData.AddDataToFile<Account>(account);

            var rub = new Rub();

            Console.ReadKey();
        }
    }
}
