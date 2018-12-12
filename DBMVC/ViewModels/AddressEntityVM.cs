using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMVC.ViewModels
{
    public class AddressEntityVM
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public ContactVM[] ContactVms { get; set; }

    }
}
