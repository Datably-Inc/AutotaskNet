namespace AutotaskNet.Domain.ValueObjects;

public record PicklistValue
{
    public string Value { get; set; }
    public string Label { get; set; }
    public bool IsDefaultValue { get; set; }
    public int SortOrder { get; set; }
    public string ParentValue { get; set; }
    public bool IsActive { get; set; }
    public bool IsSystem { get; set; }
}