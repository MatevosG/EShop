using EShop.Infrastructure.Command.Product;
using EShop.Product.Api.Services;
using EvebtBus.Inf.Models;
using MassTransit;

namespace EShop.Product.Api.Handlers
{
    public class CreateProductHandler : IConsumer<CreateProductEvent>
    {
        private readonly IProductService _productService;

        public CreateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<CreateProductEvent> context)
        {
            var createdProduct = new CreateProduct 
            {
              CategoryId = context.Message.CategoryId,
              ProductDescription = context.Message.ProductDescription,
              ProductName = context.Message.ProductName,
              ProductPrice = context.Message.ProductPrice,
              ProductId = context.Message.ProductId 
            };
            await _productService.AddProduct(createdProduct);
            await Task.CompletedTask;
        }
    }
}
