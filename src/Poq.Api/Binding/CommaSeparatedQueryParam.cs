using System.Diagnostics.CodeAnalysis;

namespace Poq.Api.Binding;

public class CommaSeparatedQueryParam
{
    public string[] Value { get; set; }

    public static bool TryParse(string? value, [NotNullWhen(true)] out CommaSeparatedQueryParam? filter)
    {
        try
        {
            var splitValue = value?.Split(',').ToArray();
            filter = new CommaSeparatedQueryParam { Value = splitValue! };
            return true;
        }
        catch
        {
            filter = default;
            return false;
        }
    }
}
