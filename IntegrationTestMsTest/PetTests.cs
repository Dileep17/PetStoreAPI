using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace IntegrationTest
{
    [TestFixture]
    public class PetTests
    {
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            string curDir = Directory.GetCurrentDirectory();
            Console.WriteLine("curr dir " + curDir);

            var tempbuilder = new ConfigurationBuilder()
                .SetBasePath(curDir)
                .AddJsonFile(curDir + "\\appsettings.json");

            var builder = new WebHostBuilder()
                .UseContentRoot(curDir)
                .UseConfiguration(tempbuilder.Build())
                .UseStartup<PetStoreAPI.Startup>();


            TestServer testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }


        [Test]
        public void CreatePet()
        {
            var newPet = "{'Name' : 'yamaha', 'Family' : 'Wolf'}";
            var datatoPost = new StringContent(newPet.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync("api/pets", datatoPost).Result;
            Console.WriteLine("Status code of create /pets = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
        }

        [Test]
        public void GetPets()
        {
            var response = _client.GetAsync("api/pets").Result;
            Console.WriteLine("Status code of /pets = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
        }

  

    }
}
