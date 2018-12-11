using System;
using System.Collections.Generic;
using System.Text;

namespace DBContactLibrary.Models
{
    public class AddressEntity
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();

        public override string ToString()
        {
            return $"{ID} {Street} {City} {Zip} \nContacts:\n{String.Join('\n', Contacts)}";
        }
    }
}
