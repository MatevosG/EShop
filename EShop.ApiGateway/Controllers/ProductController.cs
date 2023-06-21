using EShop.Infrastructure.Command.Product;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IBusControl _bus;

        public ProductController(IBusControl busControl)
        {
            _bus = busControl;
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
            var uri = new Uri("rabbitmq://localhost/create_product");

            var endPoint = await _bus.GetSendEndpoint(uri);

            await _bus.Send(endPoint);

            return Accepted("Product Created");
        }
    }
}
