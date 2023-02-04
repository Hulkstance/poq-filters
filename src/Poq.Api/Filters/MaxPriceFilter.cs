using Poq.Api.Domain;

namespace Poq.Api.Filters;

public class MaxPriceFilter
{
    public bool Apply(Product product, decimal maxPrice)
    {
        return product.Price <= maxPrice;
    }
}
