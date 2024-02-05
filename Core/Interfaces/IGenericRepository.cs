using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // Task<Product> GetProductByIdAsync(int id);
        Task<T> GetByIdAsync(int id);

        // Task<IReadOnlyList<Product>> GetProductsAsync();
        // Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        // Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    
    }
}