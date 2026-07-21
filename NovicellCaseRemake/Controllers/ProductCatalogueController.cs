using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovicellCaseRemake.DTOs;
using NovicellCaseRemake.Entities;
using NovicellCaseRemake.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NovicellCaseRemake.Controllers
{

    //Is this class a singleton?
    [ApiController]
    [Route("[controller]")]
    public class ProductCatalogueController : ControllerBase, IProductCatalogueController //do i need view support. Make interface for the require methods
    {
        private readonly IServiceProvider _serviceProvider;


        public ProductCatalogueController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        [HttpGet("{id:int}")] //change Routes to something else. figure URI best practices
        public async Task<ActionResult<ProductDTO>> GetProductDetail(int id)  //DTOs face the end user
        {
            // dont need this since Controller is already scoped
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NovicellAppDBContext>();

                ProductEntity product = await context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                ProductDTO productDTO = new ProductDTO { Id = product.ProductId, Category = product.Category, Description = product.Description, Image = product.Image, Title = product.Title, Price = product.Price };

                Console.WriteLine(productDTO);

                return Ok(productDTO);

            }
        }

        [HttpGet("{pageNumber:int}/{pagesize:int}")]
        public async Task<ActionResult<List<ProductDTO>>> GetPaginatedProducts(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 )
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NovicellAppDBContext>();

                List<ProductDTO> paginatedProducts = new List<ProductDTO>();

                List<ProductEntity> products = await context.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                if (products == null)
                {
                    return NotFound();
                }

                //converting productEnitities to productDTOs this way seems slow
                for (int i = 0; i < products.Count; i++)
                {
                    ProductDTO productDTO = new ProductDTO { Id = products[i].ProductId, Category = products[i].Category, Description = products[i].Description, Image = products[i].Image, Title = products[i].Title, Price = products[i].Price };
                    paginatedProducts.Add(productDTO);
                }

                return Ok(paginatedProducts);
            }
        }  
        //DTOs face the end user
        public async Task<ActionResult<ProductDTO[]>> GetProductList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
