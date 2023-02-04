using Poq.Api.Binding;
using Poq.Api.Contracts.Responses;
using Poq.Api.Domain;
using Poq.Api.Filters;
using Poq.Api.Mapping;
using Poq.Api.Services;
using static Microsoft.AspNetCore.Http.Results;

namespace Poq.Api.Endpoints;

public static class ProductEndpoints
{
    private const string BaseRoute = "filter";
    private const string Tag = "Products";

    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BaseRoute, GetAllAsync)
            .WithName("GetProducts")
            .Produces<FilterResponse>().Produces(400)
            .WithTags(Tag);
    }

    private static async Task<IResult> GetAllAsync(
        decimal? minPrice,
        decimal? maxPrice,
        CommaSeparatedQueryParam? size,
        CommaSeparatedQueryParam? highlight,
        IProductService productService)
    {
        var result = await productService.GetAllAsync();

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        var products = result.Data.Products
            .ToProducts()
            .ToList();

        var filterResponse = ApplyFilters(products, minPrice, maxPrice, size?.Value, highlight?.Value);

        return Ok(filterResponse);
    }

    private static FilterResponse ApplyFilters(
        List<Product> products,
        decimal? minPrice,
        decimal? maxPrice,
        string[]? size,
        string[]? highlight)
    {
        var filter = new ProductFilter();
        var filteredProducts = products.ToList();

        var minPriceFilter = new MinPriceFilter();
        if (minPrice.HasValue)
        {
            filteredProducts = filter.FilterProducts(filteredProducts, p => minPriceFilter.Apply(p, minPrice.Value));
        }

        var maxPriceFilter = new MaxPriceFilter();
        if (maxPrice.HasValue)
        {
            filteredProducts = filter.FilterProducts(filteredProducts, p => maxPriceFilter.Apply(p, maxPrice.Value));
        }

        var sizeFilter = new SizeFilter();
        if (size is { Length: > 0 })
        {
            filteredProducts = filter.FilterProducts(filteredProducts, p => sizeFilter.Apply(p, size));
        }

        var highlightFilter = new HighlightFilter();
        if (highlight is { Length: > 0 })
        {
            filteredProducts = filter.FilterProducts(filteredProducts, p => highlightFilter.Apply(p, highlight));
        }

        return new FilterResponse
        {
            Filter = new Filter
            {
                MinPrice = filteredProducts.Count > 0 ? filteredProducts.Min(x => x.Price) : 0,
                MaxPrice = filteredProducts.Count > 0 ? filteredProducts.Max(x => x.Price) : 0,
                Sizes = filteredProducts.SelectMany(x => x.Sizes).Distinct().ToArray(),
                MostCommonWords = GetMostCommonWords(products)
            },
            Products = filteredProducts
        };
    }

    private static string[] GetMostCommonWords(List<Product> products, int excludeTop = 5, int count = 10)
    {
        var words = products
            .Select(x => x.Description)
            .SelectMany(description => description.Split(' '))
            .ToList();

        var frequency = words.GroupBy(x => x)
            .Select(g => new { Word = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Skip(excludeTop)
            .Take(count)
            .Select(x => x.Word)
            .ToArray();

        return frequency;
    }
}
