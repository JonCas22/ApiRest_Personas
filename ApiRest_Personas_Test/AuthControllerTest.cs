using ApiRest_Personas.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiRest_Personas_Test
{
    public class AuthControllerTest
    {
        HttpClient client;

        public AuthControllerTest()
        {
            this.client = new HttpClient();
        }

        [Fact]
        public async Task TestAuthorized_WhenCalled_WithCorrectUser()
        {
            var data = JsonConvert.SerializeObject(new
            {
                username = "joncas",
                password = "Passw0rd"
            });


            HttpResponseMessage response = await client.PostAsync("https://localhost:5001/login", new StringContent(data, Encoding.UTF8, "application/json"));

            Assert.Equal((int)HttpStatusCode.OK, (int)response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Users>(result);

        }

        [Fact]
        public async Task TestUnAuthorized_WhenCalled_WithIncorrectUser()
        {

            var data = JsonConvert.SerializeObject(new
            {
                username = "prueba",
                password = "passprueba"
            });


            HttpResponseMessage response = await client.PostAsync("https://localhost:5001/login", new StringContent(data.ToString(), Encoding.UTF8, "application/json"));

            Assert.Equal((int)HttpStatusCode.Unauthorized, (int)response.StatusCode);

        }
    }
}
