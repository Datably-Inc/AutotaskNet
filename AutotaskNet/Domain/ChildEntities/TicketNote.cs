using System.ComponentModel.DataAnnotations;

namespace AutotaskNet.Domain.ChildEntities;

/// <summary>
/// Documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/Entities/TicketNotesEntity.htm
/// Swagger: https://webservices3.autotask.net/atservicesrest/swagger/ui/index#/TicketNotesChild
/// </summary>
public class TicketNote : AutotaskChildEntity
{
    protected override string ParentEndpoint => "Tickets";
    public override string Endpoint => "Notes";
    protected override bool CanCreate => true;
    protected override bool CanUpdate => true;
    protected override bool CanDelete => false;
    protected override bool CanQuery => true;
    protected override bool CanGetEntityInformation => true;

    public DateTime CreateDateTime { get; set; }
    public int? CreatedByContactId { get; set; }
    public required int CreatorResourceId { get; set; }
    [MaxLength(32000)] public required string Description { get; set; }
    public int? ImpersonatorCreatorResourceId { get; set; }
    public int? ImpersonatorUpdaterResourceId { get; set; }
    public DateTime LastActivityDate { get; set; }
    public required int NoteType { get; set; }
    public required int Publish { get; set; }
    public required int TicketId { get; set; }
    [MaxLength(250)] public required string Title { get; set; }
}