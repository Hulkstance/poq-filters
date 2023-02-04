using Poq.Api.Domain;

namespace Poq.Api.Contracts.Responses;

public class Filter
{
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public string[] Sizes { get; set; }
    public string[] MostCommonWords { get; set; }
}

public class FilterResponse
{
    public Filter Filter { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
