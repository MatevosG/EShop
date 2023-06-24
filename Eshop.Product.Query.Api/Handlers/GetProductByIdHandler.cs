using EShop.Infrastructure.Query;
using EShop.Product.DataProvide.Services;
using MassTransit;

namespace Eshop.Product.Query.Api.Handlers
{
    public class GetProductByIdHandler : IConsumer<GetProductById>
    {
        private IProductService _productService;

        public GetProductByIdHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<GetProductById> context)
        {
            var product = await _productService.GetProduct(context.Message.ProductId);
            await context.RespondAsync(product);
        }
    }
}
