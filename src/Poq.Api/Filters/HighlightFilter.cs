using Poq.Api.Domain;

namespace Poq.Api.Filters;

public class HighlightFilter
{
    public bool Apply(Product product, string[] keywords)
    {
        foreach (var word in keywords)
        {
            product.Description = product.Description.Replace(word, $"<em>{word}</em>");
        }

        return true;
    }
}
