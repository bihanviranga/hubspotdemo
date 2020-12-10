using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using proto.Models;
using proto.Client;

namespace proto.Pages
{
    public class HubspotIndexModel : PageModel
    {

        static readonly HttpClient client = new HttpClient();
        public ArrayList Users = new ArrayList();
        private string apiKey = "786e6eab-99f9-4aea-baf5-6ea20b7bc449";

        public async Task OnGetAsync()
        {
            // on GET, populate Users
            Users = await HubspotClient.ContactsGetAsync();
        }

        public async Task<IActionResult> OnPostAddUserAsync(User user)
        {
            await HubspotClient.ContactsPostAsync(user);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userId)
        {
            string uri = String.Format("https://api.hubapi.com/crm/v3/objects/contacts/{0}?hapikey={1}", userId, apiKey);
            var response = await client.DeleteAsync(uri);
            return RedirectToPage("./Index");
        }
    }
}