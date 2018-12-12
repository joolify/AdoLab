using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMVC.ViewModels
{
    public class UpdateContactVM
    {
        public int ID { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
