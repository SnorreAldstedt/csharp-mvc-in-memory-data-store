using exercise.wwwapi.Data;
using exercise.wwwapi.DTOs;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAllAsync()
        {

            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetByIDAsync(int id)
        {
            var result = await _db.Products.FindAsync(id);
            return result;
        }

        public async Task<Product> AddProduct(NewProduct product)
        {
            Product productInstance = new Product();
            productInstance.Name = product.Name;
            productInstance.Category = product.Category;
            productInstance.Price = product.Price;

            await _db.Products.AddAsync(productInstance);
            await _db.SaveChangesAsync();

            return productInstance;
        }

        public async Task<Product> DeleteAsync(int id)
        {
            var entity = await _db.Products.FindAsync(id);
            if(entity != null)
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }
            return entity;
        }


        public async Task<Product> UpdateAsync(int id, NewProductPut product)
        {
            //Product productInstance = new Product();

            var entity = await _db.Products.FindAsync(id);


            if (entity != null)
            {
                if (product.Name != null) entity.Name = product.Name;
                if (product.Category != null) entity.Category = product.Category;
                if (product.Price != null) entity.Price = product.Price.Value;
                _db.Products.Update(entity);
                await _db.SaveChangesAsync();
            }
            return entity;
        }
    }
}
