using BankSystem.Models;
using BankSystem.Services;
using System;
using System.Collections.Generic;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            var employees = new BankServices<Employee> { EmployeeList = new List<Employee>() };
            var clients = new BankServices<Client> { ClientList = new List<Client>() };

            //var Names = new List<String>() { "John", "James", "Nathan", "Jonathan", "Brandon", "Samuel",
            //    "Christian", "Benjamin", "Austin", "Jack", "Aaron", "Emily", "Emma", "Ashley", "Isabella",
            //    "Abigail", "Hannah", "Chloe", "Ella", "Kaitlyn", "Leslie", "Avery", "Rebecca"};
            //var Surnames = new List<String>() { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis",
            //    "Miller", "Wilson", "White", "Martin", "Garsia", "Robinson", "Clarc", "Lewis", "Lee",
            //    "Walker", "Scott", "Green", "Adams", "Carter", "Turner", "Parker", "Hill" };

            ////заполнение коллекций
            //while (clients.ClientList.Count <= 10000)
            //{
            //    var newClient = new Client()
            //    {
            //        PassportId = rnd.Next(1000000, 9999999),
            //        BirthDate = RandomBirthDate(),
            //        Name = Names[rnd.Next(Names.Count)],
            //        Surname = Surnames[rnd.Next(Surnames.Count)]
            //    };
            //    clients.Add(newClient);
            //}
            //while (employees.EmployeeList.Count <= 1000)
            //{
            //    var newEmployee = new Employee()
            //    {
            //        PassportId = rnd.Next(1000000, 9999999),
            //        BirthDate = RandomBirthDate(),
            //        Name = Names[rnd.Next(Names.Count)],
            //        Surname = Surnames[rnd.Next(Surnames.Count)],
            //        Position = "position" + rnd.Next(1000)
            //    };
            //    employees.Add(newEmployee);
            //}

            var client1 = new Client()
            {
                PassportId = 1723489,
                BirthDate = RandomBirthDate(),
                Name = "John",
                Surname = "Carter",
            };
            var client2 = new Client()
            {
                PassportId = 4826480,
                BirthDate = RandomBirthDate(),
                Name = "Rebecca",
                Surname = "Lee",
            };

            var account1 = new Account { CurrencyType = new Rup(), CurrencyAmount = 10000 };
            var account2 = new Account { CurrencyType = new Eur(), CurrencyAmount = 15 };
            var account3 = new Account { CurrencyType = new Mdl(), CurrencyAmount = 1000 };

            var dict = new Dictionary<Client, List<Account>>
            {
                { client1, new List<Account> { account1, account2 } },
                { client2, new List<Account> { account3 } }
            };

            //словарь в файл
            clients.DictionaryToFile(dict);

            //файл в словарь
            var dict2 = clients.DictionaryFromFile("C:\\Users\\Irina\\source\\repos\\DexPractice\\BankSystem");

            //ищем клиента и сотрудника в файле
            var client = new Client()
            {
                PassportId = 3388882,
                //BirthDate = Convert.ToDateTime("19.10.1970 0:00:00"),
                //Name = "Ashley",
                //Surname = "Brown"
            };
            var employee =new Employee()
            {
                PassportId = 8174005,
                //BirthDate = Convert.ToDateTime("30.09.1968 0:00:00"),
                //Name = "Emma",
                //Surname = "Green",
                //Position = "position61"
            };

            var searchedClient = clients.FindClient(client);
            var searchedEmployee = employees.FindEmployee(employee);

            Console.ReadKey();
        }

        public static DateTime RandomBirthDate()
        {
            var _start = new DateTime(1940, 1, 1);
            var _finish = new DateTime(2005, 1, 1);
            var _range = (_finish - _start).Days;
            var _rnd = new Random();

            return _start.AddDays(_rnd.Next(_range));
        }
    }
}
