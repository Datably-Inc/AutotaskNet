using System.ComponentModel.DataAnnotations;

namespace AutotaskNet.Domain.RootEntities;

/// <summary>
/// Documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/Entities/TicketNotesEntity.htm
/// Swagger: https://webservices3.autotask.net/atservicesrest/swagger/ui/index#/TicketNotes
/// </summary>
public class TicketNoteRoot : AutotaskRootEntity
{
    public override string Endpoint => "TicketNotes";
    protected override bool CanCreate => false;
    protected override bool CanUpdate => false;
    protected override bool CanDelete => false;
    protected override bool CanQuery => true;
    protected override bool CanGetEntityInformation => true;

    public DateTime CreateDateTime { get; set; }
    public int? CreatedByContactId { get; set; }
    public int? CreatorResourceId { get; set; }
    [MaxLength(32000)] public required string Description { get; set; }
    public int? ImpersonatorCreatorResourceId { get; set; }
    public int? ImpersonatorUpdaterResourceId { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int NoteType { get; set; }
    public int Publish { get; set; }
    public int TicketId { get; set; }
    [MaxLength(250)] public required string Title { get; set; }
}