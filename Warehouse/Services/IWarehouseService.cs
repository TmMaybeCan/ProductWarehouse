using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public interface IWarehouseService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(string id);
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
