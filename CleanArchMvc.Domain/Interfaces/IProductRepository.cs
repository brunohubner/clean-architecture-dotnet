using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int? id);
    Task<Product> Create(Product product);
    Task<Product> Update(Product product);
    Task<Product> Remove(Product product);
}