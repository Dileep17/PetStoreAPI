using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace IntegrationTest.InMemoryTest
{
    [TestFixture]
    class InMemoryGetPetTest
    {
        private  HttpClient _client;
        private  CustomWebApplicationFactory<PetStoreAPI.Startup> _factory;

        [SetUp]
        public void InMemoryGetPetTests()
        {
            var factory = new CustomWebApplicationFactory<PetStoreAPI.Startup>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _factory = factory;
        }

        [Test]
        public void GetPets()
        {
            var response = _client.GetAsync("api/pets").Result;
            Console.WriteLine("Status code of /pets = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
        }

        [Test]
        public void CreatePet()
        {
            var newPet = "{'Name' : 'yamaha1', 'Family' : 'Wolf1'}";
            var datatoPost = new StringContent(newPet.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync("api/pets", datatoPost).Result;
            Console.WriteLine("Status code of create /pets = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
        }

        [Test]
        public void GetPet()
        {
            var response = _client.GetAsync("api/pets/1").Result;
            Console.WriteLine("Status code of /pets = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
        }
    }
}
