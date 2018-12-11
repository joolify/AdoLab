using System;
using System.Linq;
using System.Runtime.InteropServices;
using DBContactLibrary;
using DBContactLibrary.Models;

namespace DBContactTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLRepository sqlRepository = new SQLRepository();
            /*
            //Contact
            int id = sqlRepository.CreateContact("199001227-2236", "Maria", "Svensson");
            Console.WriteLine(id);
            Contact contact = sqlRepository.ReadContact(id);
            Console.WriteLine(contact);

            sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);
            sqlRepository.DeleteContact(id);
            sqlRepository.UpdateContact(id, "199001223-2235", "Anders", "Svensson");
            Console.WriteLine();
            sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);

            //ContactInformation
            int id = sqlRepository.CreateContactInformation("test5", 1);
            Console.WriteLine(id);
            ContactInformation contactInfo = sqlRepository.ReadContactInformation(id);
            Console.WriteLine(contactInfo);
            Console.WriteLine();
            sqlRepository.ReadAllContactInformations().ForEach(Console.WriteLine);
            //sqlRepository.UpdateContactInformation(id, "test4", 3);
            sqlRepository.DeleteContactInformation(id);

            Console.WriteLine();
            sqlRepository.ReadAllContactInformations().ForEach(Console.WriteLine);

            //ContactToAddress
            int id = sqlRepository.CreateContactToAddress(12, 3);
            Console.WriteLine(id);
            ContactToAddress contactToAddress = sqlRepository.ReadContactToAddress(id);
            Console.WriteLine(contactToAddress);
            Console.WriteLine();
            sqlRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);
            Console.WriteLine();
            //sqlRepository.UpdateContactToAddress(id, 14, 3);
            sqlRepository.DeleteContactToAddress(id);

            sqlRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);
            Console.WriteLine(sqlRepository.ReadContactEntity(1));
            sqlRepository.ReadAllContactEntities().ForEach(e => Console.WriteLine(e + "\n--------------"));
            */
            sqlRepository.ReadAllAddressEntities().ForEach(e => Console.WriteLine(e + "\n--------------"));

        }
    }
}
