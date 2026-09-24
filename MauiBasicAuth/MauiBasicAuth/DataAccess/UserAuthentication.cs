using System.Net.Http.Headers;
using System.Text;

namespace MauiBasicAuth.DataAccess
{
    public class UserAuthentication
    {
        public bool AuthenticateUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                // Address of the running authentication API.
                client.BaseAddress = new Uri("https://localhost:44394/");

                // Create Basic Authentication credentials.
                var authToken =
                    Convert.ToBase64String(
                        Encoding.UTF8.GetBytes($"{username}:{password}"));

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", authToken);

                // Call the protected Values endpoint.
                var endpoint = "api/Values";

                var response = client.GetAsync(endpoint).Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}