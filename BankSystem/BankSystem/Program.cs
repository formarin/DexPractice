using BankSystem.Models;
using BankSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = new BankServices<Employee> { employeeList = new List<Employee>() };
            var clients = new BankServices<Client> { clientList = new List<Client>() };

            var locker = new object();
            var fakeDataService = new FakeDataService();
            var rnd = new Random();

            //алгоритм добавления клиентов в банковскую систему и одновременный 
            //вывод в консоль списка уже зарегистрированных клиентов
            for (int i = 0; i < 100; i++)
            {
                var client = new Client();

                ThreadPool.QueueUserWorkItem(_ =>
                {
                    lock (locker)
                    {
                        client = fakeDataService.GenerateClient();
                        clients.Add(client);
                    }
                    Thread.Sleep(2000);
                });
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    lock (locker)
                    {
                        Console.WriteLine($"{clients.clientList.Count}");
                        foreach (var item in clients.clientList)
                        {
                            Console.WriteLine($"{item.PassportId} {item.BirthDate} {item.Name} {item.Surname}");
                        }
                    }
                    Thread.Sleep(2000);
                });
            }

            Console.ReadKey();
        }
    }
}