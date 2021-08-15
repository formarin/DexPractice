using BankSystem.Exceptions;
using BankSystem.Models;
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

        public string path = Path.Combine("C:", "Users", "Irina", "source", "repos", "DexPractice", "BankSystem");
        private string strSepar = "⁂";

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
                if (!ClientList.Contains(client))
                {
                    ClientList.Add(client);
                    Console.WriteLine("Registeration completed successfully.");

                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    directoryInfo.Create();

                    var text = ($"{client.PassportId}{strSepar}{client.BirthDate}{strSepar}{client.Surname}{strSepar}{client.Name}\n");
                    using (FileStream fileStream = new FileStream($"{path}\\ListOfClients.txt", FileMode.Append))
                    {
                        byte[] array = System.Text.Encoding.Default.GetBytes(text);
                        fileStream.Write(array, 0, array.Length);
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
                if (!EmployeeList.Contains(person as Employee))
                {
                    EmployeeList.Add(person as Employee);
                    Console.WriteLine("Registeration completed successfully.");

                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    directoryInfo.Create();

                    var text = ($"{employee.PassportId}{strSepar}{employee.BirthDate}{strSepar}{employee.Surname}{strSepar}{employee.Name}{strSepar}{employee.Position}\n");
                    using (FileStream fileStream = new FileStream($"{path}\\ListOfEmployees.txt", FileMode.Append))
                    {
                        byte[] array = System.Text.Encoding.Default.GetBytes(text);
                        fileStream.Write(array, 0, array.Length);
                    }
                }
                else
                {
                    Console.WriteLine("Employee with the same passport number is already registered.");
                }
            }
        }

        public void DictionaryToFile(Dictionary<Client, List<Account>> dict)
        {
            foreach (var pair in dict)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();

                var text = $"{pair.Key.PassportId}{strSepar}{pair.Key.BirthDate}{strSepar}{pair.Key.Surname}{strSepar}{pair.Key.Name}{strSepar}";
                foreach (var account in pair.Value)
                {
                    text += $"{account.CurrencyType}{strSepar}{account.CurrencyAmount}{strSepar}";
                }
                text += "\n";

                using (FileStream fileStream = new FileStream($"{path}\\ClientsAndAccountsDictionary.txt", FileMode.Append))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(text);
                    fileStream.Write(array, 0, array.Length);
                }
            }
        }

        public Dictionary<Client, List<Account>> DictionaryFromFile(string path)
        {
            var _dict = new Dictionary<Client, List<Account>>();
            string readText;

            using (FileStream fileStream = File.OpenRead($"{path}\\ClientsAndAccountsDictionary.txt"))
            {
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                readText = System.Text.Encoding.Default.GetString(array);
            }

            string[] arr = readText.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                string[] arr2 = arr[i].Split(strSepar);

                if (arr2[0] == "")
                {
                    break;
                }
                else
                {
                    var client = new Client
                    {
                        PassportId = Convert.ToInt32(arr2[0]),
                        BirthDate = Convert.ToDateTime(arr2[1]),
                        Surname = arr2[2],
                        Name = arr2[3]
                    };

                    var list = new List<Account>();
                    for (int j = 0, k = 4; j < (Convert.ToInt32(arr2.Length) - 4) / 2; j++, k += 2)
                    {
                        var account = new Account() { CurrencyAmount = Convert.ToDouble(arr2[k + 1]) };
                        var CurrType = arr2[k];
                        if (CurrType.Substring(CurrType.Length - 3) == "Rup")
                        {
                            account.CurrencyType = new Rup();
                        }
                        else if (CurrType.Substring(CurrType.Length - 3) == "Mdl")
                        {
                            account.CurrencyType = new Mdl();
                        }
                        else if (CurrType.Substring(CurrType.Length - 3) == "Eur")
                        {
                            account.CurrencyType = new Eur();
                        }
                        list.Add(account);
                    }
                    _dict.Add(client, list);
                }
            }

            return _dict;
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
            string readText;
            if (person is Client)
            {
                var list = new List<Client>();

                using (FileStream fileStream = File.OpenRead($"{path}\\ListOfClients.txt"))
                {
                    byte[] array = new byte[fileStream.Length];
                    fileStream.Read(array, 0, array.Length);
                    readText = System.Text.Encoding.Default.GetString(array);
                }

                string[] arr = readText.Split('\n');
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] arr2 = arr[i].Split(strSepar);

                    if (arr2[0] == "")
                    {
                        break;
                    }
                    else
                    {
                        var client = new Client
                        {
                            PassportId = Convert.ToInt32(arr2[0]),
                            BirthDate = Convert.ToDateTime(arr2[1]),
                            Surname = arr2[2],
                            Name = arr2[3]
                        };

                        list.Add(client);
                    }
                }

                return list.Find(item => item.PassportId == person.PassportId);
            }
            else if (person is Employee)
            {
                var list = new List<Employee>();

                using (FileStream fileStream = File.OpenRead($"{path}\\ListOfEmployees.txt"))
                {
                    byte[] array = new byte[fileStream.Length];
                    fileStream.Read(array, 0, array.Length);
                    readText = System.Text.Encoding.Default.GetString(array);
                }

                string[] arr = readText.Split('\n');
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] arr2 = arr[i].Split(strSepar);

                    if (arr2[0] == "")
                    {
                        break;
                    }
                    else
                    {
                        var employee = new Employee
                        {
                            PassportId = Convert.ToInt32(arr2[0]),
                            BirthDate = Convert.ToDateTime(arr2[1]),
                            Surname = arr2[2],
                            Name = arr2[3],
                            Position= arr2[4]
                        };

                        list.Add(employee);
                    }
                }

                return list.Find(item => item.PassportId == person.PassportId);
            }
            return null;
        }
    }
}
