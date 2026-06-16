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
            while (cancellationToken.IsCancellationRequested)
            {
                Debug.WriteLine("GetERPProductData: Working behind the scenes...");

            }
        }

        public async Task backgroundWorker_GetERPData() //gets erp products
        {


            Console.WriteLine("Starting work...");

            var progress = new Progress<int>(percent =>
            {
                Console.WriteLine("$progress: {percent}%");
            });

            await Task.Run(() => GetERPProductData(progress));


            //maybe use queue
            Console.WriteLine("Work completed");
            
        }

        public async void GetERPProductData(IProgress<int> progress) 
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
