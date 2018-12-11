using System;
using System.Collections.Generic;
using System.Text;

namespace DBContactLibrary.Models
{
    public class ContactToAddress
    {
        public int ID { get; set; }
        public int ContactID { get; set; }
        public int AddressID { get; set; }

        public override string ToString()
        {
            return $"{ID} {ContactID} {AddressID}";
        }
    }
}