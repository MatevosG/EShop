using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;

namespace EShop.Product.DataProvide.Repositories
{
    public interface IProductRepository
    {
        Task<ProductCreated> GetProduct(string productId);
        Task<ProductCreated> AddProduct(CreateProduct product);
    }
}
