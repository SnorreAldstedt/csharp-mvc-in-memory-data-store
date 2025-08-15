using exercise.wwwapi.DTOs;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapPost("/", AddProduct);
            products.MapGet("/{id}", GetProductByID);
            products.MapDelete("/{id}", Delete);
            products.MapPut("/{id}", Update);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IRepository repository)
        {
            var results = await repository.GetAllAsync();
            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, NewProduct product)
        {
            try
            {
                var result = await repository.AddProduct(product);
                string url = $"https://localhost:7188/products/{result.Id}";
                return TypedResults.Created(url, result);
            }
            catch
            {
                return TypedResults.BadRequest(new { Error = "Wrong type" });
            }

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductByID(IRepository repository, int id)
        {
            var result = await repository.GetByIDAsync(id);

            if (result == null)
            {
                return TypedResults.NotFound(result);
            }
            return TypedResults.Ok(result); 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Delete(IRepository repository, int id)
        {
            var result = await repository.DeleteAsync(id);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Update(IRepository repository, int id, NewProductPut model)
        {
            var update = await repository.UpdateAsync(id, model);

            if (update == null)
            {
                return TypedResults.NotFound(update);
            }
            return TypedResults.Ok(update);
        }
    }
}
