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
            var employees = new BankServices<Employee> { EmployeeList = new List<Employee>() };
            var clients = new BankServices<Client> { ClientList = new List<Client>() };

            //заполнение коллекций
            while (clients.ClientList.Count <= 10000)
            {
                var newClient = clients.GenerateClient();
                clients.Add(newClient);
            }
            while (employees.EmployeeList.Count <= 1000)
            {
                var newEmployee = employees.GenerateEmployee();
                employees.Add(newEmployee);
            }

            //заполнение словаря
            var client1 = clients.GenerateClient();
            var client2 = clients.GenerateClient();

            var account1 = new Account { CurrencyType = new Rup(), CurrencyAmount = 10000 };
            var account2 = new Account { CurrencyType = new Eur(), CurrencyAmount = 15 };
            var account3 = new Account { CurrencyType = new Mdl(), CurrencyAmount = 1000 };

            var dict = new Dictionary<Client, List<Account>>
            {
                { client1, new List<Account> { account1, account2 } },
                { client2, new List<Account> { account3 } }
            };

            //словарь в файл и обратно
            var dictPath = Path.Combine("C:", "Users", "Irina", "source", "repos", "DexPractice",
                "BankSystem", "BankSystem", "FileFolder", "ClientsAndAccountsDictionary.txt");

            clients.AddDictionaryToFile(dict, dictPath);
            var desDict = clients.GetDictionaryFromFile(dictPath);
        }
    }
}
