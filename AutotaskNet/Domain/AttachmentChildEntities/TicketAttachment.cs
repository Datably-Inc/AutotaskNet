namespace AutotaskNet.Domain.AttachmentChildEntities;

/// <summary>
/// Documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/Entities/TicketAttachmentsEntity.htm
/// Swagger: https://webservices3.autotask.net/atservicesrest/swagger/ui/index#/TicketAttachmentsChild
/// </summary>
public class TicketAttachment : AutotaskAttachmentChildEntity
{
    protected override string ParentEndpoint => "Tickets";

    protected override bool CanCreate => true;
    protected override bool CanUpdate => false;
    protected override bool CanDelete => true;
    protected override bool CanQuery => true;
    protected override bool CanGetEntityInformation => false;

    public DateTime AttachDate { get; set; }
    public long? AttachedByContactId { get; set; }
    public required long AttachedByResourceId { get; set; }
    public required string AttachmentType { get; set; }
    public string ContentType { get; set; }
    public required int CreatorType { get; set; }
    public double FileSize { get; set; }
    public required string FullPath { get; set; }
    public int? ImpersonatorCreatorResourceId { get; set; }
    public long? OpportunityId { get; set; }
    public int? ParentAttachmentId { get; set; }
    public long ParentId { get; set; }
    public required int Publish { get; set; }
    public required int TicketId { get; set; }
    public int? TicketNoteId { get; set; }
    public int? TimeEntryId { get; set; }
    public required string Title { get; set; }
    public required string? Data { get; set; }
}