using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtController : Controller
    {

        private readonly IConfiguration configuration;

        public ExtController(IConfiguration configuration)
        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json");
            this.configuration = configuration; 
        }


        [HttpGet("{id}")]
        [HttpGet]
        public string GetById(int id)
        {
            HttpClient client = new HttpClient();
            var uri = new UriBuilder(configuration["UserApiUrl"])
            {
                Path = $"/api/users/{id}"
            }.Uri;
            var response = client.GetAsync(uri).Result;
            Console.WriteLine("Status code of /user = " + response.StatusCode);
            return response.Content.ReadAsStringAsync().Result.ToString();
        }
    }
}
