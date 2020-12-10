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
        public ArrayList Users = new ArrayList();

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
            await HubspotClient.ContactsDeleteAsync(userId);
            return RedirectToPage("./Index");
        }
    }
}