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

            sqlRepository.DeleteAllContactInformations();
            sqlRepository.DeleteAllContactsToAddresses();
            sqlRepository.DeleteAllContacts();
            sqlRepository.DeleteAllAddresses();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("CREATE");
            Console.WriteLine("-------------------------------");
            int cid1 = sqlRepository.CreateContact("19620601-1234", "Håkan", "Johansson");
            int cid2 = sqlRepository.CreateContact("19780805-1234", "Pontus", "Wittenmark");
            int cid3 = sqlRepository.CreateContact("19760809-1234", "Marilyn", "Comillas");

            Console.WriteLine("Read Created Contacts");
            Console.WriteLine(sqlRepository.ReadContact(cid1));
            Console.WriteLine(sqlRepository.ReadContact(cid2));
            Console.WriteLine(sqlRepository.ReadContact(cid3));

            int aid1 = sqlRepository.CreateAddress("Borgarfjordsgatan 4", "Kista", "164 10");
            int aid2 = sqlRepository.CreateAddress("Norgegatan 14", "Kista", "164 33");
            int aid3 = sqlRepository.CreateAddress("Kungsgatan 58", "Stockholm", "110 10");

            Console.WriteLine("\nRead Created Addresses");
            Console.WriteLine(sqlRepository.ReadAddress(aid1));
            Console.WriteLine(sqlRepository.ReadAddress(aid2));
            Console.WriteLine(sqlRepository.ReadAddress(aid3));

            int c2aid1 = sqlRepository.CreateContactToAddress(cid1, aid1);
            int c2aid2 = sqlRepository.CreateContactToAddress(cid3, aid2);
            int c2aid3 = sqlRepository.CreateContactToAddress(cid2, aid3);

            Console.WriteLine("\nRead Created ContactToAddresses");
            Console.WriteLine(sqlRepository.ReadContactToAddress(c2aid1));
            Console.WriteLine(sqlRepository.ReadContactToAddress(c2aid2));
            Console.WriteLine(sqlRepository.ReadContactToAddress(c2aid3));

            int ciid1 = sqlRepository.CreateContactInformation("070 464 74 32", cid1);
            int ciid2 = sqlRepository.CreateContactInformation("073 938 44 30", cid2);
            int ciid3 = sqlRepository.CreateContactInformation("072 123 45 67", null);

            Console.WriteLine("\nRead Created ContactInformation");
            Console.WriteLine(sqlRepository.ReadContactInformation(ciid1));
            Console.WriteLine(sqlRepository.ReadContactInformation(ciid2));
            Console.WriteLine(sqlRepository.ReadContactInformation(ciid3));

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("\nRead All Contacts");
            sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);

            Console.WriteLine("\nRead All Addresses");
            sqlRepository.ReadAllAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nRead All ContactsToAddresses");
            sqlRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nRead All ContactInformation");
            sqlRepository.ReadAllContactInformations().ForEach(Console.WriteLine);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("DELETE");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("\nCreate ContactToBeDeleted");
            var cid4 = sqlRepository.CreateContact("delete", "me", "please");
            sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);
            sqlRepository.DeleteContact(cid4);
            Console.WriteLine();
            sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate AddressToBeDeleted");
            var aid4 = sqlRepository.CreateAddress("delete", "me", "please");
            sqlRepository.ReadAllAddresses().ForEach(Console.WriteLine);
            sqlRepository.DeleteAddress(aid4);
            Console.WriteLine();
            sqlRepository.ReadAllAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactToAddressToBeDeleted");
            var c2aid4 = sqlRepository.CreateContactToAddress(cid3, aid3);
            sqlRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);
            sqlRepository.DeleteContactToAddress(c2aid4);
            Console.WriteLine();
            sqlRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactInformationToBeDeleted");
            var ciid4 = sqlRepository.CreateContactInformation("deleteme", cid1);
            sqlRepository.ReadAllContactInformations().ForEach(Console.WriteLine);
            sqlRepository.DeleteContactInformation(ciid4);
            Console.WriteLine();
            sqlRepository.ReadAllContactInformations().ForEach(Console.WriteLine);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("UPDATE");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("\nCreate ContactToBeUpdated");
            var cid5 = sqlRepository.CreateContact("update", "me", "please");
            sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);
            sqlRepository.UpdateContact(cid5, "I'm", "now", "updated");
            Console.WriteLine();
            sqlRepository.ReadAllContacts().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate AddressToBeUpdated");
            var aid5 = sqlRepository.CreateAddress("update", "me", "please");
            sqlRepository.ReadAllAddresses().ForEach(Console.WriteLine);
            sqlRepository.UpdateAddress(aid5, "I'm", "now", "updated");
            Console.WriteLine();
            sqlRepository.ReadAllAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactToAddressToBeUpdated");
            var c2aid5 = sqlRepository.CreateContactToAddress(cid3, aid3);
            sqlRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);
            sqlRepository.UpdateContactToAddress(c2aid5, cid2, aid2);
            Console.WriteLine();
            sqlRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactInformationToBeUpdated");
            var ciid5 = sqlRepository.CreateContactInformation("updateme", cid1);
            sqlRepository.ReadAllContactInformations().ForEach(Console.WriteLine);
            sqlRepository.UpdateContactInformation(ciid5, "I'm updated", cid2);
            Console.WriteLine();
            sqlRepository.ReadAllContactInformations().ForEach(Console.WriteLine);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ CONTACT ENTITY");
            Console.WriteLine("-------------------------------");
            Console.WriteLine(sqlRepository.ReadContactEntity(cid1));
            Console.WriteLine();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ ADDRESS ENTITY");
            Console.WriteLine("-------------------------------");
            Console.WriteLine(sqlRepository.ReadAddressEntity(aid1));
            Console.WriteLine();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ ALL CONTACT ENTITIES");
            Console.WriteLine("-------------------------------");
            sqlRepository.ReadAllContactEntities().ForEach(e => Console.WriteLine(e + "\n--------------"));
            Console.WriteLine();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ ALL ADDRESS ENTITIES");
            Console.WriteLine("-------------------------------");
            sqlRepository.ReadAllAddressEntities().ForEach(e => Console.WriteLine(e + "\n--------------"));
        }
    }
}
