using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovicellCaseRemake.DTOs;
using NovicellCaseRemake.Interfaces;

namespace NovicellCaseRemake.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCatalogueController : Controller, IProductCatalogueController //do i need view support. Make interface for the require methods
    {
        // GET: ProductCatalogueController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductCatalogueController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult<ProductDTO>> GetProductDetail(string id)  //DTOs face the end user
        {
            //get data from a database setup in entity framework
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ProductDTO[]>> GetProductList(string id)
        {
            throw new NotImplementedException();
        }


        // GET: ProductCatalogueController/Create
        /*public ActionResult Create()
        {
            return View();
        }
        */
        // POST: ProductCatalogueController/Create
        
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        // GET: ProductCatalogueController/Edit/5
        /*public ActionResult Edit(int id)
        {
            return View();
        }
        */


        // POST: ProductCatalogueController/Edit/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
        // GET: ProductCatalogueController/Delete/5
        /*public ActionResult Delete(int id)
        {
            return View();
        }
        */
        // POST: ProductCatalogueController/Delete/5
        
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
