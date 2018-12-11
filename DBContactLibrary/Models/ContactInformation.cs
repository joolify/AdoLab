using System;
using System.Collections.Generic;
using System.Text;

namespace DBContactLibrary.Models
{
    public class ContactInformation
    {
        public int ID { get; set; }
        public string Info { get; set; }
        public int? ContactID { get; set; }

        public override string ToString()
        {
            return $"{ID} {Info} {ContactID}";
        }
    }
}
