using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutotaskNet.Domain.Requests.Queries;

/// <summary>
/// For more information on each operator, see Autotask's documentation: https://autotask.net/help/developerhelp/content/apis/rest/API_Calls/REST_Basic_Query_Calls.htm#List2
/// </summary>
[JsonConverter(typeof(OperatorJsonConverter))]
public class Operator
{
    public static Operator Equal => new("eq");
    public static Operator NotEqual => new("noteq");
    public static Operator GreaterThan => new("gt");
    public static Operator GreaterThanOrEqual => new("gte");
    public static Operator LessThan => new("lt");
    public static Operator LessThanOrEqual => new("lte");
    public static Operator BeginsWith => new("beginsWith");
    public static Operator EndsWith => new("endsWith");
    public static Operator Contains => new("contains");
    public static Operator Exists => new(ExistsConst);
    public static Operator NotExists => new(NotExistsConst);
    public static Operator In => new("in");
    public static Operator NotIn => new("notIn");

    internal const string ExistsConst = "exist";
    internal const string NotExistsConst = "notExist";

    internal string Value { get; }

    private static string[] Values =
    [
        "eq",
        "noteq",
        "gt",
        "gte",
        "lt",
        "lte",
        "beginsWith",
        "endsWith",
        "contains",
        ExistsConst,
        NotExistsConst,
        "in",
        "notIn"
    ];

    private Operator(string value)
    {
        if (!Values.Contains(value))
            throw new InvalidOperationException(
                $"{value} is an invalid value for {nameof(Operator)}. " +
                $"Use one of the following: {string.Join(", ", Values)}");

        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static bool TryParse(string value, out Operator? result)
    {
        try
        {
            result = new Operator(value);
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }
}

internal class OperatorJsonConverter : JsonConverter<Operator>
{
    public override Operator? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Operator.TryParse(reader.GetString()!, out var result);
        return result;
    }

    public override void Write(Utf8JsonWriter writer, Operator value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}