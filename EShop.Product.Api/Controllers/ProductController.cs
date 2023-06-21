using EShop.Infrastructure.Command.Product;
using EShop.Product.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EShop.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string productId)
        {
            var product = await _productService.GetProduct(productId);
            return Ok(product);
        }
        [HttpPost]
        [ProducesResponseType(typeof(CreateProduct), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromForm] CreateProduct product)
        {
            var addedProduct = await _productService.AddProduct(product);
            return Ok(addedProduct);
        }
    }
}
