using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;
using EShop.Infrastructure.Query;
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
        private readonly IRequestClient<GetProductById> _request;

        public ProductController(IPublishEndpoint publishEndpoint, IRequestClient<GetProductById> request)
        {
            _publishEndpoint = publishEndpoint;
            _request = request;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string productId)
        {
            var product = await _request.GetResponse<ProductCreated>(new GetProductById { ProductId = productId });

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateProduct createProduct)
        {
            await _publishEndpoint.Publish(createProduct);
            return Accepted("Product Created");
        }
    }
}
