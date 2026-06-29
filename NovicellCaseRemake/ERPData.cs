using NovicellCaseRemake.DTOs;
using NovicellCaseRemake.Entities;
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

        private readonly IServiceProvider _serviceProvider;
        
        public ERPData(IHttpClientFactory httpClientFactory, IConfiguration config, IServiceProvider serviceProvider) 
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken) //how does cancelation token work
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

            //Implement a queue for this
            var products = await JsonSerializer.DeserializeAsync<List<ProductDTO>>(contentStream);

            //Is there a more performant way of converting products to product entities?
            List<ProductEntity> productEntities = new List<ProductEntity>();

            for (int i = 0; i < products.Count; i++)
            {
                ProductEntity productEntity= new ProductEntity{ ProductId = products[i].Id, Category = products[i].Category, Description = products[i].Description, Image = products[i].Image, Title = products[i].Title, Price = products[i].Price };
                productEntities.Add(productEntity);
            }


            Console.WriteLine(products);
            //save data to database

            using (var scope = _serviceProvider.CreateScope()) 
            {
                var context = scope.ServiceProvider.GetRequiredService<NovicellAppDBContext>();

                await context.Products.AddRangeAsync(productEntities);

                await context.SaveChangesAsync();
            }
            //not sure how good continously creating new dbconnections is. is there another way of adding a list of entries


        }
        
    }
}
