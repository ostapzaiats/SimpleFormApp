using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SimpleFormApp.Models;
using System.Xml;

namespace SimpleFormApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Street { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public int ZipCode { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var person = new Person
                {
                    Id = Id,
                    FirstName = FirstName,
                    LastName = LastName,
                };

                var address = new Address
                {
                    Street = Street,
                    City = City,
                    ZipCode = ZipCode,
                    PersonId = person.Id
                };

                SavePersonToJsonFile(person);
                SaveAddressToJsonFile(address);

                Message = $"Saved person with id {Id} : {FirstName} {LastName}, address: {Street}, {City}, {ZipCode}";
            }

            return Page();
        }

        private void SavePersonToJsonFile(Person person)
        {
            string json = JsonConvert.SerializeObject(person);
            System.IO.File.WriteAllText("person.json", json);
        }

        private void SaveAddressToJsonFile(Address address)
        {
            string json = JsonConvert.SerializeObject(address);
            System.IO.File.WriteAllText("address.json", json);
        }
    }
}