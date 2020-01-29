using Newtonsoft.Json;
using System;

namespace WPFApp.Domain.Models.Configurations
{
    public class Authentication
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expirationDate")]
        public DateTime Expiration { get; set; }

        public bool IsValid() => Expiration > DateTime.Now;

    }
}
