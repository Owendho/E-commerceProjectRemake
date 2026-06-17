using NovicellCaseRemake.DTOs;
using System.Buffers.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;

namespace NovicellCaseRemake
{
    public class ERPData : BackgroundService
    {

        private readonly IConfiguration _config;

        private readonly IHttpClientFactory _httpClientFactory;

        
        public ERPData(IHttpClientFactory httpClientFactory, IConfiguration config) 
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Debug.WriteLine("GetERPProductData: Working behind the scenes...");
                Console.WriteLine("GetERPProductData: Working behind the scenes...");
                GetERPProductData();
                await Task.Delay(5000, cancellationToken);

            }
        }

        public async void GetERPProductData() 
        {
            var path = _config["ErpSettings:ProductsPath"]; // move this out to program.cs
            var httpClient = _httpClientFactory.CreateClient("ERPClient");
            using HttpResponseMessage response = await httpClient.GetAsync($"{path}");

            response.EnsureSuccessStatusCode();

            using var contentStream = await response.Content.ReadAsStreamAsync();

            var products = await JsonSerializer.DeserializeAsync<List<ProductDTO>>(contentStream);

            Console.WriteLine(products);
            //save data to database
        }
        
    }
}
