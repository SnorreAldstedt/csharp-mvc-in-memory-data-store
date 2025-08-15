using exercise.wwwapi.DTOs;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> AddProduct(NewProduct product);

        Task<Product> GetByIDAsync(int id);
        Task<Product> DeleteAsync(int id);

        Task<Product> UpdateAsync(int id, NewProductPut product);
    }
}
