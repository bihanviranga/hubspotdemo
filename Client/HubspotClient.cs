using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using proto.Models;

namespace proto.Client
{
    public static class HubspotClient
    {
        static readonly HttpClient client = new HttpClient();
        public static string ApiKey = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";

        public static async Task<ArrayList> ContactsGetAsync()
        {
            // an arraylist to store users
            ArrayList users = new ArrayList();
            // make the GET request
            var uri = String.Format("https://api.hubapi.com/crm/v3/objects/contacts?limit=10&archived=false&hapikey={0}", ApiKey);
            string response = await client.GetStringAsync(uri);
            // parse the response
            var parsedResponse = JObject.Parse(response);
            var resultsArray = parsedResponse["results"];
            foreach (var res in resultsArray)
            {
                // create User objects and populate the users arraylist
                User user = new User();
                user.id = (string)res["id"];
                user.firstname = (string)res["properties"]["firstname"];
                user.lastname = (string)res["properties"]["lastname"];
                user.email = (string)res["properties"]["email"];
                users.Add(user);
            }

            return users;
        }

        public static async Task ContactsPostAsync(User user)
        {
            // make the User object into JSON
            string jsonUser = JsonConvert.SerializeObject(user).ToString();
            // prepare request
            string jsonBody = "{\"properties\": " + jsonUser + "}";
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            // make the POST request
            string uri = String.Format("https://api.hubapi.com/crm/v3/objects/contacts?hapikey={0}", ApiKey);
            var response = await client.PostAsync(uri, content);
            // TODO: check the response's status code to see if the request was successful
        }

        public static async Task ContactsDeleteAsync(string userId)
        {
            // make the DELETE request
            string uri = String.Format("https://api.hubapi.com/crm/v3/objects/contacts/{0}?hapikey={1}", userId, ApiKey);
            var response = await client.DeleteAsync(uri);
            // TODO: check the response's status code to see if the request was successful
        }
    }
}
