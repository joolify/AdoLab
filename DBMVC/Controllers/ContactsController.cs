using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContactLibrary;
using DBContactLibrary.Models;
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
            AddressVM[] addressVms = SQLRepository.ReadAllAddresses()?
                .Select(a => new AddressVM
                {
                    ID = a.ID,
                    City = a.City,
                    Street = a.Street,
                    Zip = a.Zip
                })
                .ToArray();
            return View(addressVms);
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
            SQLRepository.ReadAllContactsToAddresses()?
                .Where(c2a => c2a.AddressID == id)
                .Select(c2a => c2a.ID)
                .ToList()
                .ForEach(i => SQLRepository.DeleteContactToAddress(i));

            SQLRepository.DeleteAddress(id);
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            ContactVM[] contactVms = SQLRepository.ReadAllContacts()?
                .Select(c => new ContactVM
                {
                    ID = c.ID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SSN = c.SSN
                })
                .ToArray();
            return View(contactVms);
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
            SQLRepository.ReadAllContactsToAddresses()?
                .Where(c2a => c2a.ContactID == id)
                .Select(c2a => c2a.ID)
                .ToList()
                .ForEach(i => SQLRepository.DeleteContactToAddress(i));

            SQLRepository.ReadAllContactInformations()?
                .Where(ci => ci.ContactID == id)
                .Select(ci => ci.ID)
                .ToList()
                .ForEach(i => SQLRepository.DeleteContactInformation(i));

            SQLRepository.DeleteContact(id);
            return RedirectToAction(nameof(Contacts));
        }

        [HttpGet]
        public IActionResult AddressEntities()
        {
            AddressEntityVM[] addressEntityVms = SQLRepository.ReadAllAddressEntities()?
                .Select(a => new AddressEntityVM
                {
                    City = a.City,
                    Street = a.Street,
                    Zip = a.Zip,
                    ContactVms = a.Contacts
                        .Select(c => new ContactVM
                        {
                            SSN = c.SSN,
                            FirstName = c.FirstName,
                            LastName = c.LastName
                        })
                        .ToArray()
                })
                .ToArray();
            return View(addressEntityVms);
        }
        [HttpGet]
        public IActionResult ContactEntites()
        {
            ContactEntityVM[] contactEntityVms = SQLRepository.ReadAllContactEntities()?
                .Select(c => new ContactEntityVM
                {
                    SSN = c.SSN,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    AddressVms = c.Addresses?
                        .Select(a => new AddressVM
                        {
                            ID = a.ID,
                            City = a.City,
                            Street = a.Street,
                            Zip = a.Zip
                        })
                        .ToArray(),
                    ContactInformationVms = c.ContactInformations?
                        .Select(ci => new ContactInformationVM
                        {
                            ContactID = ci.ContactID,
                            Info = ci.Info
                        })
                        .ToArray()
                })
                .ToArray();
            return View(contactEntityVms);
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
                ContactItems = SQLRepository.ReadAllContacts()?
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
                ContactItems = SQLRepository.ReadAllContacts()?
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = $"{c.FirstName} {c.LastName}"

                    }).ToArray(),
                AddressItems = SQLRepository.ReadAllAddresses()?
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

        [HttpGet]
        public IActionResult Populate()
        {
            int cid1 = SQLRepository.CreateContact("19620601-1234", "Håkan", "Johansson");
            int cid2 = SQLRepository.CreateContact("19780805-1234", "Pontus", "Wittenmark");
            int cid3 = SQLRepository.CreateContact("19760809-1234", "Marilyn", "Comillas");

            int aid1 = SQLRepository.CreateAddress("Borgarfjordsgatan 4", "Kista", "164 10");
            int aid2 = SQLRepository.CreateAddress("Norgegatan 14", "Kista", "164 33");
            int aid3 = SQLRepository.CreateAddress("Kungsgatan 58", "Stockholm", "110 10");

            int c2aid1 = SQLRepository.CreateContactToAddress(cid1, aid1);
            int c2aid2 = SQLRepository.CreateContactToAddress(cid3, aid2);
            int c2aid3 = SQLRepository.CreateContactToAddress(cid2, aid3);

            int ciid1 = SQLRepository.CreateContactInformation("070 464 74 32", cid1);
            int ciid2 = SQLRepository.CreateContactInformation("073 938 44 30", cid2);
            int ciid3 = SQLRepository.CreateContactInformation("072 123 45 67", null);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteAll()
        {
            SQLRepository.DeleteAllContactInformations();
            SQLRepository.DeleteAllContactsToAddresses();
            SQLRepository.DeleteAllContacts();
            SQLRepository.DeleteAllAddresses();
            return RedirectToAction(nameof(Index));
        }
    }
}