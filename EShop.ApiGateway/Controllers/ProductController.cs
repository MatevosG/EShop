using EShop.Infrastructure.Command.Product;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductController(IPublishEndpoint publishEndpoint)
        {
            
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromForm] CreateProduct createProduct)
        {
            await Task.CompletedTask;
            return Accepted("Product Created");
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateProduct createProduct)
        {
            await _publishEndpoint.Publish(createProduct);
            return Accepted("Product Created");
        }
      
    }
}
