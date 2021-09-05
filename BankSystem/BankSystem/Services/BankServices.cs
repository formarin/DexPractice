using BankSystem.Exceptions;
using BankSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BankSystem.Services
{
    public class BankServices<T> where T : IPerson
    {
        public List<Client> clientList { get; set; }
        public List<Employee> employeeList { get; set; }
        public Dictionary<String, List<Account>> clientsAndAccountsList { get; set; }

        public string path = Path.Combine("C:", "Users", "Irina", "source",
          "repos", "DexPractice", "FileFolder");

        public delegate double exchangeHandler<Currency>(double moneyAmount, Currency currency1, Currency currency2);
        private exchangeHandler<Currency> _exchangeHandler;
        public void DelegateRegister(exchangeHandler<Currency> exchangeHandler)
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

        public void MoneyTransfer(int sum, Account donorAccount, Account recipientAccount, exchangeHandler<Currency> _exchangeHandler)
        {
            var _exch = new Exchange();
            if (donorAccount.CurrencyAmount >= sum)
            {
                donorAccount.CurrencyAmount -= sum;
                recipientAccount.CurrencyAmount += _exch.CurrencyExchange<Currency, Currency>(sum, donorAccount.CurrencyType, recipientAccount.CurrencyType);
            }
            else
            {
                throw new NotEnoughMoneyException("Not enough money.");
            }
        }

        public void AddAccount(Client client, Account account)
        {
            if (clientsAndAccountsList.ContainsKey(client.PassportId))
            {
                clientsAndAccountsList[client.PassportId].Add(account);
            }
            else
            {
                if ((DateTime.Now.Year - client.BirthDate.Year) < 16)
                {
                    throw new AgeLimitsExeption("Client registration is available from the age of 16 years.");
                }
                else
                {
                    clientsAndAccountsList.Add(client.PassportId, new List<Account> { account });
                }
            }
        }

        public void Add(T person)
        {
            if (person is Client)
            {
                var client = person as Client;
                if (clientList.Contains(client) == false)
                {
                    clientList.Add(client);
                }
                else
                {
                    Console.WriteLine("Client with the same passport number is already registered.");
                }
            }
            else
            {
                var employee = person as Employee;
                if (employeeList.Contains(employee) == false)
                {
                    employeeList.Add(employee);
                }
                else
                {
                    Console.WriteLine("Employee with the same passport number is already registered.");
                }
            }
        }

        //public void Add(T person)
        //{
        //    if (person is Client)
        //    {
        //        var client = person as Client;
        //        string serClient;
        //        ///**/if (clientList.Contains(client) == false)
        //        if (FindClient(client) == null)
        //        {
        //            ///**/clientList.Add(client);
        //            serClient = JsonConvert.SerializeObject(client);
        //            var directoryInfo = new DirectoryInfo(path);
        //            if (!directoryInfo.Exists)
        //            {
        //                directoryInfo.Create();
        //            }
        //            using (var fileStream = new FileStream($"{path}\\ListOfClients.txt", FileMode.Append))
        //            {
        //                byte[] array = System.Text.Encoding.Default.GetBytes(serClient);
        //                fileStream.Write(array, 0, array.Length);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Client with the same passport number is already registered.");
        //        }
        //    }
        //    else
        //    {
        //        var employee = person as Employee;
        //        string serEmployee;
        //        ///**/if (employeeList.Contains(employee) == false)
        //        if (FindEmployee(employee) == null)
        //        {
        //            ///**/employeeList.Add(employee);
        //            serEmployee = JsonConvert.SerializeObject(employee);
        //            var directoryInfo = new DirectoryInfo(path);
        //            if (!directoryInfo.Exists)
        //            {
        //                directoryInfo.Create();
        //            }
        //            using (var fileStream = new FileStream($"{path}\\ListOfEmployees.txt", FileMode.Append))
        //            {
        //                byte[] array = System.Text.Encoding.Default.GetBytes(serEmployee);
        //                fileStream.Write(array, 0, array.Length);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Employee with the same passport number is already registered.");
        //        }
        //    }
        //}

        public void AddDictionaryToFile(Dictionary<string, List<Account>> dict, string dictPath)
        {
            using (var streamWriter = new StreamWriter(dictPath))
            {
                streamWriter.Write(JsonConvert.SerializeObject(dict));
            }
        }

        public Dictionary<string, List<Account>> GetDictionaryFromFile(string dictPath)
        {
            using (var streamReader = new StreamReader(dictPath))
            {
                var desDict = JsonConvert.DeserializeObject<Dictionary<string, List<Account>>>(streamReader.ReadToEnd());
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

                return clientList.Find(item => item.PassportId == person.PassportId);
            }
            else
            {
                var employee = person as Employee;

                return employeeList.Find(item => item.PassportId == person.PassportId);
            }
        }

        //private IPerson Find<U>(U person) where U : IPerson
        //{
        //    if (person is Client)
        //    {
        //        var client = person as Client;
        //        var desClientList = new List<Client>();
        //        var clientListPath = Path.Combine("C:", "Users", "Irina", "source", "repos", "DexPractice",
        //            "BankSystem", "BankSystem", "FileFolder", "ListOfClients.txt");

        //        var directoryInfo = new DirectoryInfo(path);
        //        if (!directoryInfo.Exists)
        //        {
        //            directoryInfo.Create();
        //        }

        //        using (var fileStream = new FileStream($"{path}\\ListOfClients.txt", FileMode.Open))
        //        {
        //            byte[] array = new byte[fileStream.Length];
        //            fileStream.Read(array, 0, array.Length);
        //            if (!(array.Length == 0))
        //            {
        //                desClientList = JsonConvert.DeserializeObject<List<Client>>(Convert.ToString(array));
        //            }
        //        }

        //        return desClientList.Find(item => item.PassportId == person.PassportId);
        //    }
        //    else
        //    {
        //        var employee = person as Employee;
        //        var desEmployeeList = new List<Employee>();
        //        var employeeListPath = Path.Combine("C:", "Users", "Irina", "source", "repos", "DexPractice",
        //            "BankSystem", "BankSystem", "FileFolder", "ListOfEmployees.txt");

        //        var directoryInfo = new DirectoryInfo(employeeListPath);
        //        if (!directoryInfo.Exists)
        //        {
        //            directoryInfo.Create();
        //        }

        //        using (var fileStream = new FileStream($"{path}\\ListOfEmployees.txt", FileMode.Open))
        //        {
        //            byte[] array = new byte[fileStream.Length];
        //            fileStream.Read(array, 0, array.Length);
        //            if (!(array.Length == 0))
        //            {
        //                desEmployeeList = JsonConvert.DeserializeObject<List<Employee>>(Convert.ToString(array));
        //            }
        //        }

        //        return desEmployeeList.Find(item => item.PassportId == person.PassportId);
        //    }
        //}
    }
}
