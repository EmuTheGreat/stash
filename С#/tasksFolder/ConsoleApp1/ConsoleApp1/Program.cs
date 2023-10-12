using System.Collections.Generic;
using System;
using System.Linq;

namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = new List<Contact>
            {
                new Contact("Игорь", "Николаев", 79990000001, "igor@example.com"),
                new Contact("Сергей", "Довлатов", 79990000010, "serge@example.com"),
                new Contact("Анатолий", "Карпов", 79990000011, "anatoly@example.com"),
                new Contact("Валерий", "Леонтьев", 79990000012, "valera@example.com"),
                new Contact("Сергей", "Брин", 799900000013, "serg@example.com"),
                new Contact("Иннокентий", "Смоктуновский", 799900000013, "innokentii@example.com")
            };

            var sortedPhoneBook = phoneBook.OrderBy(x => x.Name)
                .ThenBy(x => x.LastName);

            while (true)
            {
                var keyChar = Console.ReadKey().KeyChar;
                var pageNumber = int.Parse(keyChar.ToString());
                Console.Clear();

                var page = sortedPhoneBook.Skip((pageNumber - 1) * 2).Take(2);

                foreach (var contact in page)
                    Console.WriteLine(contact.Name + " " + contact.LastName + ": " + contact.PhoneNumber);
            }
        }
    }

    public class Contact
    {
        public string Name { get; }
        public string LastName { get; }
        public long PhoneNumber { get; }
        public string Email { get; }

        public Contact(string name, string lastName, long phoneNumber, String email)
        {
            Name = name;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
