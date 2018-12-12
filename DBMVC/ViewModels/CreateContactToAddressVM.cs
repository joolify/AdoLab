using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBMVC.ViewModels
{
    public class CreateContactToAddressVM
    {
        [Display(Name = "Contact:")]
        public SelectListItem[] ContactItems { get; set; }
        public int SelectedContactValue { get; set; }
 
        [Display(Name = "Address:")]
        public SelectListItem[] AddressItems { get; set; }
        public int SelectedAddressValue { get; set; }
    }
}
