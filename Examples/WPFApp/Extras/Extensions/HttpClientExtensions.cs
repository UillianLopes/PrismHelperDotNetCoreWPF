using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Extras.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> Read<T>(this HttpResponseMessage message) where T : class
        {
            message.EnsureSuccessStatusCode();

            if (!(message.Content is HttpContent content && await content.ReadAsStringAsync() is string stream))
                return null;
            
            return JsonConvert.DeserializeObject<T>(stream);
        }

        public static IObservable<T> Post<T>(this HttpClient client, string uri, object content) where T : class
        {
            StringContent jsonContent = null;

            if (content != null)
            {
                var json = JsonConvert.SerializeObject(content);
                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return Observable
                .FromAsync(() => client.PostAsync(uri, jsonContent))
                .Select((message) => Observable.FromAsync(() => message.Read<T>()))
                .Switch();

        }

        public static IObservable<T> Put<T>(this HttpClient client, string uri, object content = null) where T : class
        {
            StringContent jsonContent = null;

            if (content != null)
            {
                var json = JsonConvert.SerializeObject(content);
                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return Observable
                .FromAsync(() => client.PutAsync(uri, jsonContent))
                .Select((message) => Observable.FromAsync(() => message.Read<T>()))
                .Switch();
        }

        public static IObservable<T> Get<T>(this HttpClient client, string uri) where T : class => Observable
                .FromAsync(() => client.GetAsync(uri))
                .Select((message) => Observable.FromAsync(() => message.Read<T>()))
                .Switch();
    }
}
