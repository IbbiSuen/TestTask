using APITests.APICore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APITests.APICore
{
    public abstract class APIBase : IDisposable
    {
        protected HttpClient _client;
        protected LoggerHelper _loggerHelper;

        protected APIBase()
        {
            _client = CreateHttpClient();
            _loggerHelper = new LoggerHelper();
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://fakerestapi.azurewebsites.net/api/v1/")
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            return httpClient;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}