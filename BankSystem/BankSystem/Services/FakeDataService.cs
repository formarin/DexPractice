using BankSystem.Models;
using Bogus;
using System;

namespace BankSystem.Services
{
    public class FakeDataService
    {
        public Client GenerateClient()
        {
            return new Faker<Client>("en")
                .RuleFor(x => x.PassportId, f => Convert.ToString(f.Random.Int(1000000, 9999999)))
                .RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth)
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Surname, f => f.Person.LastName);
        }

        public Employee GenerateEmployee()
        {
            return new Faker<Employee>("en")
                .RuleFor(x => x.PassportId, f => Convert.ToString(f.Random.Int(1000000, 9999999)))
                .RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth)
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Surname, f => f.Person.LastName)
                .RuleFor(x => x.Position, f => f.Name.JobTitle());
        }

    }
}
