using Poq.Api.Domain;

namespace Poq.Api.Filters;

public class MinPriceFilter
{
    public bool Apply(Product product, decimal minPrice)
    {
        return product.Price >= minPrice;
    }
}
