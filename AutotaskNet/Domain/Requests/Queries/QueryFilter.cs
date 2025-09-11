using System.Text.Json.Serialization;
using AutotaskNet.Utilities;

namespace AutotaskNet.Domain.Requests.Queries;

/// <summary>
/// For information on using filters, see Autotask's documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/API_Calls/REST_Basic_Query_Calls.htm#GET%C2%A0with
/// </summary>
public record QueryFilter : IValidation
{
    [JsonPropertyName("filter")] public List<Filter> Filters { get; set; }

    public static QueryFilter All => new()
    {
        Filters = [new Filter { Operator = Operator.NotEqual, Field = nameof(AutotaskEntity.Id), Value = -1 }]
    };

    public void AssertValidState()
    {
        if (!Filters.Any())
            throw new AutotaskNetCoreValidationException($"{nameof(QueryFilter)} does not have any filters.");

        foreach (var filter in Filters) filter.AssertValid();
    }

    /// <summary>
    /// For information on using filters, see Autotask's documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/API_Calls/REST_Basic_Query_Calls.htm#GET%C2%A0with
    /// </summary>
    public record Filter
    {
        [JsonPropertyName("op")] public Operator Operator { get; set; }

        public string Field { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Value { get; set; }

        [JsonPropertyName("udf")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsUdf { get; set; } = false;

        public void AssertValid()
        {
            switch (Operator.Value)
            {
                case Operator.ExistsConst:
                case Operator.NotExistsConst:
                    if (Value != null)
                        throw new AutotaskNetCoreValidationException(
                            $"{nameof(Filter)} is using a non-comparison operator, but a value was given.\n" +
                            $"For more information on non-comparison operators, see Autotask's documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/API_Calls/REST_Basic_Query_Calls.htm#Non-comp");
                    break;

                default:
                    if (Value == null)
                        throw new AutotaskNetCoreValidationException(
                            $"{nameof(Filter)} is using a comparison operator, but a value was not given.\n" +
                            $"For more information on non-comparison operators, see Autotask's documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/API_Calls/REST_Basic_Query_Calls.htm#Comparis");
                    break;
            }
        }
    }
}