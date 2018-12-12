using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DBContactLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBMVC.ViewModels
{
    public class CreateContactInformationVM
    {

        [Display(Name = "Information:")]
        public string Info { get; set; }
        [Display(Name = "Contact:")]
        public SelectListItem[] ContactItems { get; set; }
        public int SelectedContactValue { get; set; }
    }
}