using EShop.Infrastructure.Command.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Add([FromForm] CreateProduct createProduct)
        {
            await Task.CompletedTask;
            return Accepted("Product Created");
        }
    }
}
