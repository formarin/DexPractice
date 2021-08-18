using System;

namespace BankSystem.Models
{
    [Serializable]
    public class Client : IPerson
    {
        public string PassportId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Client))
            {
                return false;
            }
            return ((Client)obj).PassportId == PassportId;
        }

        public static bool operator ==(Client client1, Client client2)
        {
            return client1.Equals(client2);
        }
        public static bool operator !=(Client client1, Client client2)
        {
            return !client1.Equals(client2);
        }

        public override int GetHashCode()
        {
            return PassportId.GetHashCode();
        }
    }
}
