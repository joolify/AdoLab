using System;
using System.Collections.Generic;
using System.Text;

namespace DBContactLibrary.Models
{
    public class ContactEntity
    {
        public int ID { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<ContactInformation> ContactInformations { get; set; } = new List<ContactInformation>();

        public override string ToString()
        {
            return $"{ID} {SSN} {FirstName} {LastName} \nAddresses:\n{String.Join('\n', Addresses)}\nContactInformation:\n{String.Join('\n', ContactInformations)}";
        }
    }
}
