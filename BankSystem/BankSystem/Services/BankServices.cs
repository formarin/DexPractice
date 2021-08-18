using BankSystem.Exceptions;
using BankSystem.Models;
using Bogus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BankSystem.Services
{
    public class BankServices<T> where T : IPerson
    {
        public List<Client> ClientList { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public Dictionary<Client, List<Account>> ClientsAndAccountsList { get; set; }

        public string path = Path.Combine("C:", "Users", "Irina", "source", "repos", "DexPractice", "BankSystem", "BankSystem", "FileFolder");

        public delegate double ExchangeHandler<Currency>(double MoneyAmount, Currency Curr1, Currency Curr2);
        private ExchangeHandler<Currency> _exchangeHandler;
        public void DelegateRegister(ExchangeHandler<Currency> exchangeHandler)
        {
            try
            {
                if (exchangeHandler == null)
                {
                    throw new ArgumentNullException();
                }
                _exchangeHandler = exchangeHandler;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MoneyTransfer(int sum, Account donorAccount, Account recipientAccount, ExchangeHandler<Currency> _exchangeHandler)
        {
            var _exch = new Exchange();
            if (donorAccount.CurrencyAmount >= sum)
            {
                donorAccount.CurrencyAmount -= sum;
                recipientAccount.CurrencyAmount += _exch.CurrExch<Currency, Currency>(sum, donorAccount.CurrencyType, recipientAccount.CurrencyType);
            }
            else
            {
                throw new NotEnoughMoneyException("Not enough money.");
            }
        }

        public void AddAccount(Client client, Account account)
        {
            if (ClientsAndAccountsList.ContainsKey(client))
            {
                ClientsAndAccountsList[client].Add(account);
            }
            else
            {
                if ((DateTime.Now.Year - client.BirthDate.Year) < 16)
                {
                    throw new AgeLimitsExeption("Client registration is available from the age of 16 years.");
                }
                else
                {
                    ClientsAndAccountsList.Add(client, new List<Account> { account });
                }
            }
        }

        public void Add(T person)
        {
            if (person is Client)
            {
                var client = person as Client;
                string serClient;
                if (FindClient(client) == null)
                {
                    serClient = JsonConvert.SerializeObject(client);
                    using (var streamWriter = new StreamWriter($"{path}\\ListOfClients.txt"))
                    {
                        streamWriter.Write(JsonConvert.SerializeObject(serClient));
                    }
                }
                else
                {
                    Console.WriteLine("Client with the same passport number is already registered.");
                }
            }
            else
            {
                var employee = person as Employee;
                string serEmployee;
                if (FindEmployee(employee) == null)
                {
                    serEmployee = JsonConvert.SerializeObject(employee);
                    using (var streamWriter = new StreamWriter($"{path}\\ListOfEmployees.txt"))
                    {
                        streamWriter.Write(JsonConvert.SerializeObject(serEmployee));
                    }
                }
                else
                {
                    Console.WriteLine("Employee with the same passport number is already registered.");
                }
            }
        }

        public Client GenerateClient()
        {
            return new Faker<Client>("en")
                .RuleFor(x => x.PassportId, f => f.Random.Int(1000000, 9999999))
                .RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth)
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Surname, f => f.Person.LastName);
        }

        public Employee GenerateEmployee()
        {
            return new Faker<Employee>("en")
                .RuleFor(x => x.PassportId, f => f.Random.Int(1000000, 9999999))
                .RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth)
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Surname, f => f.Person.LastName)
                .RuleFor(x => x.Position, f => f.Name.JobTitle());
        }

        public void AddDictionaryToFile(Dictionary<Client, List<Account>> dict, string dictPath)
        {
            using (var streamWriter = new StreamWriter(dictPath))
            {
                streamWriter.Write(JsonConvert.SerializeObject(dict));
            }
        }

        public Dictionary<Client, List<Account>> GetDictionaryFromFile(string dictPath)
        {
            using (var streamReader = new StreamReader(dictPath))
            {
                /*Newtonsoft.Json.JsonSerializationException: "Could not convert string 'BankSystem.Models.Client'
                to dictionary key type 'BankSystem.Models.Client'.*/
                var desDict = JsonConvert.DeserializeObject<Dictionary<Client, List<Account>>>(streamReader.ReadToEnd());
                return desDict;
            }
        }

        public Employee FindEmployee(Employee employee)
        {
            return (Employee)Find<Employee>(employee);
        }

        public Client FindClient(Client client)
        {
            return (Client)Find<Client>(client);
        }

        private IPerson Find<U>(U person) where U : IPerson
        {
            if (person is Client)
            {
                var client = person as Client;
                List<Client> desClientList;

                var directoryInfo = new DirectoryInfo($"{path}\\ListOfClients.txt");
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                /*System.UnauthorizedAccessException: "Access to the path
                'C:\Users\Irina\source\repos\DexPractice\BankSystem\BankSystem\FileFolder\ListOfClients.txt' is denied."*/
                using (var streamReader = new StreamReader($"{path}\\ListOfClients.txt"))
                {
                    desClientList = JsonConvert.DeserializeObject<List<Client>>(streamReader.ReadToEnd());
                }

                foreach (var item in desClientList) 
                {
                    if (item.PassportId == client.PassportId)
                    {
                        return item;
                    }
                }
            }
            else
            {
                var employee = person as Employee;
                List<Employee> desEmployeeList;

                var directoryInfo = new DirectoryInfo($"{path}\\ListOfEmployees.txt");
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                /*System.UnauthorizedAccessException: "Access to the path
                'C:\Users\Irina\source\repos\DexPractice\BankSystem\BankSystem\FileFolder\ListOfEmployees.txt' is denied."*/
                using (var streamReader = new StreamReader($"{path}\\ListOfEmployees.txt"))
                {
                    desEmployeeList = JsonConvert.DeserializeObject<List<Employee>>(streamReader.ReadToEnd());
                }

                foreach (var item in desEmployeeList)
                {
                    if (item.PassportId == employee.PassportId)
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }
}
