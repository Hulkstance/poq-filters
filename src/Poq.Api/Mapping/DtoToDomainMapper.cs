using Poq.Api.Contracts.Data;
using Poq.Api.Domain;

namespace Poq.Api.Mapping;

public static class DtoToDomainMapper
{
    public static Product ToProduct(this ProductDto productDto)
    {
        return new Product
        {
            Title = productDto.Title,
            Price = productDto.Price,
            Sizes = productDto.Sizes,
            Description = productDto.Description
        };
    }

    public static IEnumerable<Product> ToProducts(this IEnumerable<ProductDto> productDto)
    {
        return productDto
            .Select(x => x.ToProduct());
    }
}
