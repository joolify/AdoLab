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
            SQLRepository.DeleteAllContactInformations();
            SQLRepository.DeleteAllContactsToAddresses();
            SQLRepository.DeleteAllContacts();
            SQLRepository.DeleteAllAddresses();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("CREATE");
            Console.WriteLine("-------------------------------");
            int cid1 = SQLRepository.CreateContact("19620601-1234", "Håkan", "Johansson");
            int cid2 = SQLRepository.CreateContact("19780805-1234", "Pontus", "Wittenmark");
            int cid3 = SQLRepository.CreateContact("19760809-1234", "Marilyn", "Comillas");

            Console.WriteLine("Read Created Contacts");
            Console.WriteLine(SQLRepository.ReadContact(cid1));
            Console.WriteLine(SQLRepository.ReadContact(cid2));
            Console.WriteLine(SQLRepository.ReadContact(cid3));

            int aid1 = SQLRepository.CreateAddress("Borgarfjordsgatan 4", "Kista", "164 10");
            int aid2 = SQLRepository.CreateAddress("Norgegatan 14", "Kista", "164 33");
            int aid3 = SQLRepository.CreateAddress("Kungsgatan 58", "Stockholm", "110 10");

            Console.WriteLine("\nRead Created Addresses");
            Console.WriteLine(SQLRepository.ReadAddress(aid1));
            Console.WriteLine(SQLRepository.ReadAddress(aid2));
            Console.WriteLine(SQLRepository.ReadAddress(aid3));

            int c2aid1 = SQLRepository.CreateContactToAddress(cid1, aid1);
            int c2aid2 = SQLRepository.CreateContactToAddress(cid3, aid2);
            int c2aid3 = SQLRepository.CreateContactToAddress(cid2, aid3);

            Console.WriteLine("\nRead Created ContactToAddresses");
            Console.WriteLine(SQLRepository.ReadContactToAddress(c2aid1));
            Console.WriteLine(SQLRepository.ReadContactToAddress(c2aid2));
            Console.WriteLine(SQLRepository.ReadContactToAddress(c2aid3));

            int ciid1 = SQLRepository.CreateContactInformation("070 464 74 32", cid1);
            int ciid2 = SQLRepository.CreateContactInformation("073 938 44 30", cid2);
            int ciid3 = SQLRepository.CreateContactInformation("072 123 45 67", null);

            Console.WriteLine("\nRead Created ContactInformation");
            Console.WriteLine(SQLRepository.ReadContactInformation(ciid1));
            Console.WriteLine(SQLRepository.ReadContactInformation(ciid2));
            Console.WriteLine(SQLRepository.ReadContactInformation(ciid3));

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("\nRead All Contacts");
            SQLRepository.ReadAllContacts().ForEach(Console.WriteLine);

            Console.WriteLine("\nRead All Addresses");
            SQLRepository.ReadAllAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nRead All ContactsToAddresses");
            SQLRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nRead All ContactInformation");
            SQLRepository.ReadAllContactInformations().ForEach(Console.WriteLine);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("DELETE");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("\nCreate ContactToBeDeleted");
            var cid4 = SQLRepository.CreateContact("delete", "me", "please");
            SQLRepository.ReadAllContacts().ForEach(Console.WriteLine);
            SQLRepository.DeleteContact(cid4);
            Console.WriteLine();
            SQLRepository.ReadAllContacts().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate AddressToBeDeleted");
            var aid4 = SQLRepository.CreateAddress("delete", "me", "please");
            SQLRepository.ReadAllAddresses().ForEach(Console.WriteLine);
            SQLRepository.DeleteAddress(aid4);
            Console.WriteLine();
            SQLRepository.ReadAllAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactToAddressToBeDeleted");
            var c2aid4 = SQLRepository.CreateContactToAddress(cid3, aid3);
            SQLRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);
            SQLRepository.DeleteContactToAddress(c2aid4);
            Console.WriteLine();
            SQLRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactInformationToBeDeleted");
            var ciid4 = SQLRepository.CreateContactInformation("deleteme", cid1);
            SQLRepository.ReadAllContactInformations().ForEach(Console.WriteLine);
            SQLRepository.DeleteContactInformation(ciid4);
            Console.WriteLine();
            SQLRepository.ReadAllContactInformations().ForEach(Console.WriteLine);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("UPDATE");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("\nCreate ContactToBeUpdated");
            var cid5 = SQLRepository.CreateContact("update", "me", "please");
            SQLRepository.ReadAllContacts().ForEach(Console.WriteLine);
            SQLRepository.UpdateContact(cid5, "I'm", "now", "updated");
            Console.WriteLine();
            SQLRepository.ReadAllContacts().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate AddressToBeUpdated");
            var aid5 = SQLRepository.CreateAddress("update", "me", "please");
            SQLRepository.ReadAllAddresses().ForEach(Console.WriteLine);
            SQLRepository.UpdateAddress(aid5, "I'm", "now", "updated");
            Console.WriteLine();
            SQLRepository.ReadAllAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactToAddressToBeUpdated");
            var c2aid5 = SQLRepository.CreateContactToAddress(cid3, aid3);
            SQLRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);
            SQLRepository.UpdateContactToAddress(c2aid5, cid2, aid2);
            Console.WriteLine();
            SQLRepository.ReadAllContactsToAddresses().ForEach(Console.WriteLine);

            Console.WriteLine("\nCreate ContactInformationToBeUpdated");
            var ciid5 = SQLRepository.CreateContactInformation("updateme", cid1);
            SQLRepository.ReadAllContactInformations().ForEach(Console.WriteLine);
            SQLRepository.UpdateContactInformation(ciid5, "I'm updated", cid2);
            Console.WriteLine();
            SQLRepository.ReadAllContactInformations().ForEach(Console.WriteLine);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ CONTACT ENTITY");
            Console.WriteLine("-------------------------------");
            Console.WriteLine(SQLRepository.ReadContactEntity(cid1));
            Console.WriteLine();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ ADDRESS ENTITY");
            Console.WriteLine("-------------------------------");
            Console.WriteLine(SQLRepository.ReadAddressEntity(aid1));
            Console.WriteLine();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ ALL CONTACT ENTITIES");
            Console.WriteLine("-------------------------------");
            SQLRepository.ReadAllContactEntities().ForEach(e => Console.WriteLine(e + "\n--------------"));
            Console.WriteLine();

            Console.WriteLine("-------------------------------");
            Console.WriteLine("READ ALL ADDRESS ENTITIES");
            Console.WriteLine("-------------------------------");
            SQLRepository.ReadAllAddressEntities().ForEach(e => Console.WriteLine(e + "\n--------------"));
        }
    }
}
