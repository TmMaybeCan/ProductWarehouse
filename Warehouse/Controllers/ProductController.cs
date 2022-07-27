using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Warehouse.Entities;
using Warehouse.Services;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        public ProductController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));    
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _warehouseService.GetAllProducts();

            if (products is null)
                return NotFound();
            return Ok(products);

        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        { 
            var product = await _warehouseService.GetProduct(id);

            if (product is null)
                return NotFound();
            return Ok(product);
        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _warehouseService.AddProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody]Product product)
        {
            return await _warehouseService.UpdateProduct(product) ? (ActionResult)Ok(true) : (ActionResult)NotFound(false);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            return await _warehouseService.DeleteProduct(id) ? (ActionResult)Ok(true) : (ActionResult)NotFound(false);
        }
    }
}
