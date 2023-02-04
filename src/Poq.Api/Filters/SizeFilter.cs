using Poq.Api.Domain;

namespace Poq.Api.Filters;

public class SizeFilter
{
    public bool Apply(Product product, string[] sizes)
    {
        return sizes.All(product.Sizes.Contains);
    }
}
