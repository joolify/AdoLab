using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMVC.ViewModels
{
    public class UpdateAddressVM
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }
}
