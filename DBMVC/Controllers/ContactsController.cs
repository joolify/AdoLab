using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContactLibrary;
using DBContactLibrary.Models;
using DBMVC.Models;
using DBMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBMVC.Controllers
{
    public class ContactsController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Addresses()
        {
            return View(SQLRepository.ReadAllAddresses().ToArray());
        }

        [HttpGet]
        public IActionResult UpdateAddressForm(int id)
        {
            Address address = SQLRepository.ReadAddress(id);
            UpdateAddressVM updateAddressVm = new UpdateAddressVM
            {
                ID = address.ID,
                City = address.City,
                Street = address.Street,
                Zip = address.Zip
            };
            return View(updateAddressVm);
        }

        [HttpPost]
        public IActionResult UpdateAddressForm(UpdateAddressVM updateAddressVm)
        {
            if (!ModelState.IsValid)
                return View(updateAddressVm);

            SQLRepository.UpdateAddress(updateAddressVm.ID, updateAddressVm.Street, updateAddressVm.City,
                updateAddressVm.Zip);
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet]
        public IActionResult DeleteAddress(int id)
        {
            SQLRepository.DeleteAddress(id);
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return View(SQLRepository.ReadAllContacts().ToArray());
        }

        [HttpGet]
        public IActionResult UpdateContactForm(int id)
        {
            Contact contact = SQLRepository.ReadContact(id);
            UpdateContactVM updateContactVm = new UpdateContactVM
            {
                ID = contact.ID,
                SSN = contact.SSN,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };
            return View(updateContactVm);
        }

        [HttpPost]
        public IActionResult UpdateContactForm(UpdateContactVM updateContactVm)
        {
            if (!ModelState.IsValid)
                return View(updateContactVm);

            SQLRepository.UpdateContact(updateContactVm.ID, updateContactVm.SSN, updateContactVm.FirstName,
                updateContactVm.LastName);
            return RedirectToAction(nameof(Contacts));
        }

        [HttpGet]
        public IActionResult DeleteContact(int id)
        {
            SQLRepository.DeleteContact(id);
            return RedirectToAction(nameof(Contacts));
        }

        [HttpGet]
        public IActionResult AddressEntities()
        {
            return View(SQLRepository.ReadAllAddressEntities().ToArray());
        }
        [HttpGet]
        public IActionResult ContactEntites()
        {
            return View(SQLRepository.ReadAllContactEntities().ToArray());
        }

        [HttpGet]
        public IActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactForm(CreateContactVM createContactVm)
        {
            if (!ModelState.IsValid)
                return View(createContactVm);

            SQLRepository.CreateContact(createContactVm.SSN, createContactVm.FirstName, createContactVm.LastName);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddressForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddressForm(CreateAddressVM createAddressVm)
        {
            if (!ModelState.IsValid)
                return View(createAddressVm);

            SQLRepository.CreateAddress(createAddressVm.Street, createAddressVm.City, createAddressVm.Zip);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ContactInformationForm()
        {
            CreateContactInformationVM createContactInformationVm = new CreateContactInformationVM
            {
                ContactItems = SQLRepository.ReadAllContacts()
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = $"{c.FirstName} {c.LastName}"

                    }).ToArray()
            };
            return View(createContactInformationVm);
        }

        [HttpPost]
        public IActionResult ContactInformationForm(CreateContactInformationVM contactInformationVm)
        {
            if (!ModelState.IsValid)
                return View(contactInformationVm);
            SQLRepository.CreateContactInformation(contactInformationVm.Info, contactInformationVm.SelectedContactValue);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ContactToAddressForm()
        {
            CreateContactToAddressVM createContactToAddressVm = new CreateContactToAddressVM
            {
                ContactItems = SQLRepository.ReadAllContacts()
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = $"{c.FirstName} {c.LastName}"

                    }).ToArray(),
                AddressItems = SQLRepository.ReadAllAddresses()
                    .Select(a => new SelectListItem
                    {
                        Value = a.ID.ToString(),
                        Text = $"{a.City}, {a.Street}"

                    }).ToArray(),
            };
            return View(createContactToAddressVm);
        }

        [HttpPost]
        public IActionResult ContactToAddressForm(CreateContactToAddressVM createContactToAddressVm)
        {
            if (!ModelState.IsValid)
                return View(createContactToAddressVm);
            
            SQLRepository.CreateContactToAddress(createContactToAddressVm.SelectedContactValue, createContactToAddressVm.SelectedAddressValue);
            return RedirectToAction(nameof(Index));
        }
    }
}