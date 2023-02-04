using Poq.Api.Domain;

namespace Poq.Api.Filters;

public class ProductFilter
{
    public List<Product> FilterProducts(List<Product> products, Func<Product, bool> filter)
    {
        return products.Where(filter).ToList();
    }
}
