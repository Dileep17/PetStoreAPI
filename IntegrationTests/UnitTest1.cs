using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests
{
    public class UnitTest1
    {
        
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetAllPets()
        {
            string curDir = Directory.GetCurrentDirectory();
            output.WriteLine("curr dir " + curDir);

            var tempbuilder = new ConfigurationBuilder()
                .SetBasePath(curDir)
                .AddJsonFile("appsettings.json");

            var builder = new WebHostBuilder()
                .UseContentRoot(curDir)
                .UseConfiguration(tempbuilder.Build())
                .UseStartup<PetStoreAPI.Startup>();
                

            TestServer testServer = new TestServer(builder);
            HttpClient client = testServer.CreateClient();

            var response = client.GetAsync("api/pets").Result;
            output.WriteLine("Status code of /pets = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            output.WriteLine(res);

        }
    }
}
