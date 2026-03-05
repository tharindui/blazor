using BlazorCrudLearning.Models;

namespace BlazorCrudLearning.Services;

/// <summary>
/// Very simple in-memory service for learning.
/// Important: Data resets when the app restarts.
/// </summary>
public class InMemoryProductService : IProductService
{
    private readonly List<Product> _products =
    [
        new Product { Id = 1, Name = "Keyboard", Price = 49.99m, Description = "Mechanical keyboard" },
        new Product { Id = 2, Name = "Mouse", Price = 24.99m, Description = "Wireless mouse" }
    ];

    private int _nextId = 3;

    public IReadOnlyList<Product> GetAll()
    {
        // Return a copy so callers don't mutate internal list accidentally.
        return _products
            .Select(Clone)
            .OrderBy(p => p.Id)
            .ToList();
    }

    public Product? GetById(int id)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        return existing is null ? null : Clone(existing);
    }

    public Product Create(Product product)
    {
        var created = Clone(product);
        created.Id = _nextId++;
        _products.Add(created);
        return Clone(created);
    }

    public bool Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index < 0)
        {
            return false;
        }

        _products[index] = Clone(product);
        return true;
    }

    public bool Delete(int id)
    {
        var removedCount = _products.RemoveAll(p => p.Id == id);
        return removedCount > 0;
    }

    private static Product Clone(Product source) => new()
    {
        Id = source.Id,
        Name = source.Name,
        Price = source.Price,
        Description = source.Description
    };
}
