using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using proto.Models;

namespace proto.Client
{
    public static class HubspotClient
    {
        static readonly HttpClient client = new HttpClient();
        private static string ApiKey = "786e6eab-99f9-4aea-baf5-6ea20b7bc449";

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
    }
}