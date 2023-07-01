using EShop.Gateway.Api.Models;
using EvebtBus.Inf.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<GetProductByIdEvent> _request;

        public ProductController(IPublishEndpoint publishEndpoint, IRequestClient<GetProductByIdEvent> request)
        {
            _publishEndpoint = publishEndpoint;
            _request = request;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(string productId)
        {
            var product = await _request.GetResponse<ProductCreatedEvent>(new GetProductByIdEvent { ProductId = productId });

            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateProductEvent createProduct)
        {
            await _publishEndpoint.Publish(createProduct);
            return Accepted("Product Created");
        }
    }
}
