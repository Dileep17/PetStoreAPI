﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using NUnit.Framework;
using PetStoreAPI;
using Stubbery;

namespace IntegrationTest
{
    [TestFixture]
    public class ExtTest
    {
        private HttpClient _client;

        private ApiStub StartStub()
        {
            var userApiStub = new ApiStub();

            userApiStub.Get(
                "/api/users/{id}",
                (request, args) =>
                {
                    if (args.Route.id == "2")
                    {
                        return "{ \"status\": 200, \"result\": { \"im\": \"mocked\" } }";
                    }

                    return "{ \"status\": 500 }";
                });
            userApiStub.Start();
            return userApiStub;
        }

        private TestServer StartApiUnderTest(ApiStub userApiStub = null)
        {

            var host = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();

            if (userApiStub != null)
            {
                host.ConfigureAppConfiguration((ctx, b) =>
                {
                    b.Add(new MemoryConfigurationSource
                    {
                        InitialData = new Dictionary<string, string>
                        {
                            // Replace the real api URL with the stub.
                            ["UserApiUrl"] = userApiStub.Address
                        }
                    });
                });
            }

            var server = new TestServer(host);

            return server;
        }

        [Test]
        public void TestExtAPIMocked()
        {
            var stub = StartStub();
            var server = StartApiUnderTest(stub);
            _client = server.CreateClient();
            var response = _client.GetAsync("/api/ext/2").Result;
            Console.WriteLine("Status code of /ext = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
        }

        [Test]
        public void TestExtAPINotMocked()
        {
            var server = StartApiUnderTest();
            _client = server.CreateClient();
            var response = _client.GetAsync("/api/ext/2").Result;
            Console.WriteLine("Status code of /ext = " + response.StatusCode);
            string res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
        }

    }
}