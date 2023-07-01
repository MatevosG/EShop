using EShop.Infrastructure.Query;
using EShop.Product.DataProvide.Services;
using EvebtBus.Inf.Models;
using MassTransit;

namespace Eshop.Product.Query.Api.Handlers
{
    public class GetProductByIdHandler : IConsumer<GetProductByIdEvent>
    {
        private IProductService _productService;

        public GetProductByIdHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<GetProductByIdEvent> context)
        {
            var product = await _productService.GetProduct(context.Message.ProductId);
            await context.RespondAsync<ProductCreatedEvent>(product);
        }
    }
}
