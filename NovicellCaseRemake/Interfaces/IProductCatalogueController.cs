using Microsoft.AspNetCore.Mvc;
using NovicellCaseRemake.DTOs;

namespace NovicellCaseRemake.Interfaces

{
    public interface IProductCatalogueController
    {
        public Task<ActionResult<ProductDTO>> GetProductDetail(string id); //how to have the async keyword in an interface?
        public Task<ActionResult<ProductDTO[]>> GetProductList(string id);
    }
}
