using System.ComponentModel.DataAnnotations;
using AutotaskNet.Domain.ValueObjects;

namespace AutotaskNet.Domain.RootEntities;

/// <summary>
/// Documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/Entities/TicketsEntity.htm
/// Swagger: https://webservices3.autotask.net/atservicesrest/swagger/ui/index#/Tickets
/// </summary>
public class Ticket : AutotaskRootEntity
{
    public override string Endpoint => "Tickets";
    protected override bool CanCreate => true;
    protected override bool CanUpdate => true;
    protected override bool CanDelete => false;
    protected override bool CanQuery => true;
    protected override bool CanGetEntityInformation => true;

    /// <summary>Picklist value</summary>
    public int? ApiVendorID { get; set; }
    
    public int? AssignedResourceID { get; set; }
    public int? AssignedResourceRoleID { get; set; }
    public int? BillingCodeID { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? ChangeApprovalBoard { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? ChangeApprovalStatus { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? ChangeApprovalType { get; set; }
    
    [MaxLength(8000)] public string ChangeInfoField1 { get; set; }
    [MaxLength(8000)] public string ChangeInfoField2 { get; set; }
    [MaxLength(8000)] public string ChangeInfoField3 { get; set; }
    [MaxLength(8000)] public string ChangeInfoField4 { get; set; }
    [MaxLength(8000)] public string ChangeInfoField5 { get; set; }
    public required int CompanyID { get; set; }
    public int? CompanyLocationID { get; set; }
    public required int? CompletedByResourceID { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int? ConfigurationItemID { get; set; }
    public int? ContactID { get; set; }
    public int? ContractID { get; set; }
    public long? ContractServiceBundleID { get; set; }
    public long? ContractServiceID { get; set; }
    public DateTime CreateDate { get; set; }
    public int? CreatedByContactID { get; set; }
    public required int CreatorResourceID { get; set; }
    
    /// <summary>Picklist value</summary>
    public required int CreatorType { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? CurrentServiceThermometerRating { get; set; }
    
    [MaxLength(8000)] public string? Description { get; set; }
    public required DateTime DueDateTime { get; set; }
    public double? EstimatedHours { get; set; }
    [MaxLength(50)] public string ExternalID { get; set; }
    public int? FirstResponseAssignedResourceID { get; set; }
    public DateTime FirstResponseDateTime { get; set; }
    public DateTime? FirstResponseDueDateTime { get; set; }
    public int? FirstResponseInitiatingResourceID { get; set; }
    public double? HoursToBeScheduled { get; set; }
    public int? ImpersonatorCreatorResourceID { get; set; }
    public bool IsAssignedToComanaged { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? IssueType { get; set; }
    
    public bool IsVisibleToComanaged { get; set; }
    public DateTime LastActivityDate { get; set; }
    
    /// <summary>Picklist value</summary>
    public required int LastActivityPersonType { get; set; }
    
    public required int LastActivityResourceID { get; set; }
    public DateTime? LastCustomerNotificationDateTime { get; set; }
    public DateTime? LastCustomerVisibleActivityDateTime { get; set; }
    public DateTime LastTrackedModificationDateTime { get; set; }
    public long? MonitorID { get; set; }
    public int? MonitorTypeID { get; set; }
    public int? OpportunityID { get; set; }
    public int? OrganizationalLevelAssociationID { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? PreviousServiceThermometerRating { get; set; }
    
    /// <summary>Picklist value</summary>
    public required int Priority { get; set; }
    
    public int? ProblemTicketId { get; set; }
    public int? ProjectID { get; set; }
    [MaxLength(50)] public string PurchaseOrderNumber { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? QueueID { get; set; }
    
    [MaxLength(32000)] public string Resolution { get; set; }
    public DateTime? ResolutionPlanDateTime { get; set; }
    public DateTime? ResolutionPlanDueDateTime { get; set; }
    public DateTime? ResolvedDateTime { get; set; }
    public DateTime? ResolvedDueDateTime { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? RmaStatus { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? RmaType { get; set; }
    
    [MaxLength(50)] public string? RmmAlertID { get; set; }
    public bool? ServiceLevelAgreementHasBeenMet { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? ServiceLevelAgreementID { get; set; }
    
    public double? ServiceLevelAgreementPausedNextEventHours { get; set; }
    public int? ServiceThermometerTemperature { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? Source { get; set; }
    
    /// <summary>Picklist value</summary>
    public required int Status { get; set; }
    
    /// <summary>Picklist value</summary>
    public int? SubIssueType { get; set; }
    
    /// <summary>Picklist value</summary>
    public int TicketCategory { get; set; }
    
    [MaxLength(50)] public string TicketNumber { get; set; }
    
    /// <summary>Picklist value</summary>
    public required int TicketType { get; set; }
    [MaxLength(250)] public required string Title { get; set; }

    [MaxLength(300)]
    public List<UserDefinedField> UserDefinedFields { get; set; } = new();
}