namespace Poq.Api.Contracts.Data;

public class ProductDto
{
    public required string Title { get; init; }

    public required decimal Price { get; init; }

    public required string[] Sizes { get; init; }

    public required string Description { get; init; }
}

public class ApiKeys
{
    public required string Primary { get; init; }

    public required string Secondary { get; init; }
}

public class GetAllProductsResponse
{
    public IEnumerable<ProductDto> Products { get; init; } = Enumerable.Empty<ProductDto>();


    public required ApiKeys ApiKeys { get; init; }
}
