using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;

namespace EShop.Product.Api.Services
{
    public interface IProductService
    {
        Task<ProductCreated> GetProduct(string productId);
        Task<ProductCreated> AddProduct(CreateProduct product);
    }
}
