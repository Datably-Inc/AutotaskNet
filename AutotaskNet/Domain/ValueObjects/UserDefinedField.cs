namespace AutotaskNet.Domain.ValueObjects;

public record UserDefinedField
{
    public string Name { get; set; }
    public string Value { get; set; }
}