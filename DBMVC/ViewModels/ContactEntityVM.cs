using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMVC.ViewModels
{
    public class ContactEntityVM
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressVM[] AddressVms { get; set; } 
        public ContactInformationVM[] ContactInformationVms { get; set; } 


    }
}
