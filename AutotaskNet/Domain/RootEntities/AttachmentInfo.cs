namespace AutotaskNet.Domain.RootEntities;

/// <summary>
/// Documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/Entities/AttachmentInfoEntity.htm
/// Swagger: https://webservices3.autotask.net/atservicesrest/swagger/ui/index#/AttachmentInfo
/// </summary>
public class AttachmentInfo : AutotaskRootEntity
{
    public override string Endpoint => "AttachmentInfo";
    protected override bool CanCreate => false;
    protected override bool CanUpdate => false;
    protected override bool CanDelete => false;
    protected override bool CanQuery => true;
    protected override bool CanGetEntityInformation => true;

    public int ArticleID { get; set; }
    public DateTime AttachDate { get; set; }
    public long AttachedByContactID { get; set; }
    public long AttachedByResourceID { get; set; }
    public string AttachmentType { get; set; }
    public int CompanyID { get; set; }
    public int CompanyNoteID { get; set; }
    public int ConfigurationItemID { get; set; }
    public int ConfigurationItemNoteID { get; set; }
    public string ContentType { get; set; }
    public int ContractID { get; set; }
    public int ContractNoteID { get; set; }
    public int CreatorType { get; set; }
    public int DocumentID { get; set; }
    public int ExpenseItemID { get; set; }
    public int ExpenseReportID { get; set; }
    public double FileSize { get; set; }
    public string FullPath { get; set; }
    public int ImpersonatorCreatorResourceID { get; set; }
    public long OpportunityID { get; set; }
    public int ParentAttachmentID { get; set; }
    public long ParentID { get; set; }
    public int ParentType { get; set; }
    public int ProjectID { get; set; }
    public int ProjectNoteID { get; set; }
    public int Publish { get; set; }
    public int ResourceID { get; set; }
    public int SalesOrderID { get; set; }
    public int TaskID { get; set; }
    public int TaskNoteID { get; set; }
    public int TicketID { get; set; }
    public int TicketNoteID { get; set; }
    public int TimeEntryID { get; set; }
    public string Title { get; set; }
    public long SoapParentPropertyId { get; set; }
}