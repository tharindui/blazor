using BlazorCrudLearning.Models;

namespace BlazorCrudLearning.Services;

/// <summary>
/// Interface for product CRUD operations.
/// Using an interface keeps components decoupled from implementation details.
/// </summary>
public interface IProductService
{
    IReadOnlyList<Product> GetAll();
    Product? GetById(int id);
    Product Create(Product product);
    bool Update(Product product);
    bool Delete(int id);
}
