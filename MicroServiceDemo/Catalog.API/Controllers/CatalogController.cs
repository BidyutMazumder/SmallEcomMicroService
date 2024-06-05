using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        private readonly IProductManager _productManager;

        public CatalogController(IProductManager productManager)
        {
            this._productManager = productManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public IActionResult GetProduct()
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("ok", products);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public IActionResult GetByCategory(string category)
        {
            try
            {
                var products = _productManager.GetByCategory(category);
                return CustomResult("ok", products);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public IActionResult SaveProduct([FromBody]Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                bool IsSaved = _productManager.Add(product);
                if (IsSaved)
                {
                    return CustomResult("Product Save Successfully", product, HttpStatusCode.Created);
                }
                else
                {
                    return CustomResult("Product save Failed", product, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("Data Not Found", HttpStatusCode.NotFound);
                }
                bool IsUpdate = _productManager.Update(product.Id, product);
                if (IsUpdate)
                {
                    return CustomResult("Product Update Successfully", product, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product Update Failed", product, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProduct([FromBody] string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return CustomResult("Data Not Found", HttpStatusCode.NotFound);
                }
                bool IsDelete = _productManager.Delete(Id);
                if (IsDelete)
                {
                    return CustomResult("Product Delete Successfully", HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product Delete Failed", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult GetById(string Id)
        {
            try
            {
                var product = _productManager.GetById(Id);
                return CustomResult("ok", product);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
