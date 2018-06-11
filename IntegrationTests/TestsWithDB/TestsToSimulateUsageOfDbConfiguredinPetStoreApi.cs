using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PetStoreAPI;

namespace IntegrationTests.TestsWithDB
{
   [TestFixture]
    public class TestsToSimulateUsageOfDbConfiguredinPetStoreApi
    {
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            //            string curDir = Directory.GetCurrentDirectory();
            //
            //            var tempbuilder = new ConfigurationBuilder()
            //                .SetBasePath(curDir)
            //                .AddJsonFile(curDir + "\\appsettings.json");
            //
            //            var builder = new WebHostBuilder()
            //                .UseContentRoot(curDir)
            //                .UseConfiguration(tempbuilder.Build())
            //                .UseStartup<PetStoreAPI.Startup>();


            var host = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();

            TestServer testServer = new TestServer(host);
            _client = testServer.CreateClient();
        }

        [Test]
        public void CreatePetTest()
        {
            var newPet = "{'Name' : 'yamaha', 'Family' : 'Wolf'}";
            var datatoPost = new StringContent(newPet.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync("api/pets", datatoPost).Result;
            string responseString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseString);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void GetPetsTest()
        {
            var response = _client.GetAsync("api/pets").Result;
            Console.WriteLine("Status code of /pets = " + response.StatusCode);
            string responseString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseString);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

  

    }
}
