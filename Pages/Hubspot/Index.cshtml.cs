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

namespace proto.Pages
{
    public class HubspotModel : PageModel
    {

        static readonly HttpClient client = new HttpClient();
        public ArrayList Users = new ArrayList();
        private string apiKey = "786e6eab-99f9-4aea-baf5-6ea20b7bc449";

        public async Task OnGetAsync()
        {
            var uri = String.Format("https://api.hubapi.com/crm/v3/objects/contacts?limit=10&archived=false&hapikey={0}", apiKey);
            string response = await client.GetStringAsync(uri);
            var parsedResponse = JObject.Parse(response);
            var resultsArray = parsedResponse["results"];
            foreach (var res in resultsArray)
            {
                User user = new User();
                user.id = (string)res["id"];
                user.firstname = (string)res["properties"]["firstname"];
                user.lastname = (string)res["properties"]["lastname"];
                user.email = (string)res["properties"]["email"];
                Users.Add(user);
            }
        }

        public async Task<IActionResult> OnPostAddUserAsync(User user)
        {
            string jsonUser = JsonConvert.SerializeObject(user).ToString();
            string jsonBody = "{\"properties\": " + jsonUser + "}";
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            string uri = String.Format("https://api.hubapi.com/crm/v3/objects/contacts?hapikey={0}", apiKey);
            var response = await client.PostAsync(uri, content);
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