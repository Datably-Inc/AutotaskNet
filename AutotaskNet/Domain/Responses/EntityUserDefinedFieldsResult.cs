using AutotaskNet.Domain.ValueObjects;

namespace AutotaskNet.Domain.Responses;

public class EntityUserDefinedFieldsResult
{
    public List<EntityUserDefinedField> Fields { get; set; }
}

public class EntityUserDefinedField
{
    public string Name { get; set; }
    public string Label { get; set; }
    public string Type { get; set; }
    public int Length { get; set; }
    public string Description { get; set; }
    public bool IsRequired { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsQueryable { get; set; }
    public bool IsReference { get; set; }
    public string ReferenceEntityType { get; set; }
    public bool IsPickList { get; set; }
    public List<PicklistValue>? PicklistValues { get; set; }
    public string PicklistParentValueField { get; set; }
    public string DefaultValue { get; set; }
    public bool IsSupportedWebhookField { get; set; }
}
