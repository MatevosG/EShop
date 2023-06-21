using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;
using EShop.Product.Api.Repositories;

namespace EShop.Product.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductCreated> AddProduct(CreateProduct product)
        {
            //product.ProductId = Guid.NewGuid();
            return await _productRepository.AddProduct(product);
        }

        public async Task<ProductCreated> GetProduct(string productId)
        {
            return await _productRepository.GetProduct(productId);
        }
    }
}
