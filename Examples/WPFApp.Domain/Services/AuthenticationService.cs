using Newtonsoft.Json;
using System.IO;
using System.Text;
using WPFApp.Domain.Extensions;
using WPFApp.Domain.Models.Configurations;

namespace WPFApp.Domain.Services
{
    public class AuthenticationService


    {
        private const string FILE = "authentication.json";

        private Authentication _authentication;
        private Authentication Authentication
        {
            get
            {
                if (_authentication == null)
                    Load();

                return _authentication;
            }
        }

        public bool HasValidAuthentication { get => Authentication?.IsValid() == true; }

        public string Token { get => HasValidAuthentication ? Authentication.Token : null; }

        private void Load()
        {
            if (!(File.Exists(FILE) && File.ReadAllText(FILE) is string json))
                return;

            _authentication = json.TryDecrypt(out string decryptedJson) ? JsonConvert.DeserializeObject<Authentication>(decryptedJson) : null;
        }

        public void Save(Authentication authentication)
        {
            if (!(JsonConvert.SerializeObject(authentication) is string json))
                return;

            if (File.Exists(FILE))
                File.Delete(FILE);

            using var stream = new StreamWriter(File.Create(FILE), Encoding.UTF8);

            stream.Write(json.TryCrypt(out string cryptedJson) ? cryptedJson : "");
        }

        public void Clear()
        {
            if (File.Exists(FILE))
                File.Delete(FILE);
        }
    }
}
