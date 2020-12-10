using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using proto.Client;

namespace proto.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string apiKey = HubspotClient.ApiKey;
        public bool keyChanged = false;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost(string apikey)
        {
            HubspotClient.ApiKey = apikey;
            apiKey = apikey;
            keyChanged = true;
        }
    }
}
