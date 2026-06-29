using Microsoft.AspNetCore.Mvc;
using NovicellCaseRemake.DTOs;

namespace NovicellCaseRemake.Interfaces

{
    public interface IProductCatalogueController
    {
        public Task<ActionResult<ProductDTO>> GetProductDetail(int id); //how to have the async keyword in an interface? am igetting the product based on the product id or its id in the database? should probably be product id
        public Task<ActionResult<ProductDTO[]>> GetProductList(string id);
    }
}
