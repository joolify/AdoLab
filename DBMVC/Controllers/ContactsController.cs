using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContactLibrary;
using Microsoft.AspNetCore.Mvc;

namespace DBMVC.Controllers
{
    public class ContactsController : Controller
    {
        SQLRepository repository = new SQLRepository();

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View(repository.ReadAllAddressEntities().ToArray());
        }
        public IActionResult Contact()
        {
            return View(repository.ReadAllContactEntities().ToArray());
        }
 
    }
}