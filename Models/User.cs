using Newtonsoft.Json;

namespace proto.Models
{
    public class User
    {
        [JsonIgnore]
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }
}