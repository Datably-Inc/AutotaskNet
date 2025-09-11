using System.ComponentModel.DataAnnotations;
using AutotaskNet.Domain.ValueObjects;

namespace AutotaskNet.Domain.RootEntities;

/// <summary>
/// Documentation: https://autotask.net/help/developerhelp/Content/APIs/REST/Entities/CompaniesEntity.htm
/// Swagger: https://webservices3.autotask.net/atservicesrest/swagger/ui/index#/Companies
/// </summary>
public class Company : AutotaskRootEntity
{
    public override string Endpoint => "Companies";
    protected override bool CanCreate => true;
    protected override bool CanUpdate => true;
    protected override bool CanDelete => false;
    protected override bool CanQuery => true;
    protected override bool CanGetEntityInformation => true;

    [MaxLength(100)] public string AdditionalAddressInformation { get; set; }
    [MaxLength(150)] public string Address1 { get; set; }
    [MaxLength(150)] public string Address2 { get; set; }
    [MaxLength(25)] public string AlternatePhone1 { get; set; }
    [MaxLength(25)] public string AlternatePhone2 { get; set; }
    public readonly int? ApiVendorID;
    public int? AssetValue { get; set; }
    public int? BillToCompanyLocationID { get; set; }
    [MaxLength(100)] public string BillToAdditionalAddressInformation { get; set; }
    [MaxLength(150)] public string BillingAddress1 { get; set; }
    [MaxLength(150)] public string BillingAddress2 { get; set; }
    public int? BillToAddressToUse { get; set; }
    [MaxLength(50)] public string BillToAttention { get; set; }
    [MaxLength(50)] public string BillToCity { get; set; }
    public int? BillToCountryID { get; set; }
    [MaxLength(128)] public string BillToState { get; set; }
    [MaxLength(50)] public string BillToZipCode { get; set; }
    [MaxLength(50)] public string City { get; set; }
    public int? Classification { get; set; }
    public int CompanyCategoryID { get; set; }
    [MaxLength(100)] public required string CompanyName { get; set; }
    [MaxLength(50)] public string CompanyNumber { get; set; }
    public required int CompanyType { get; set; }
    public int? CompetitorID { get; set; }
    public int? CountryID { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreatedByResourceID { get; set; }
    public int CurrencyID { get; set; }
    [MaxLength(25)] public string Fax { get; set; }
    public int? ImpersonatorCreatorResourceID { get; set; }
    public int InvoiceEmailMessageID { get; set; }
    public int? InvoiceMethod { get; set; }
    public bool? InvoiceNonContractItemsToParentCompany { get; set; }
    public int InvoiceTemplateID { get; set; }
    public bool IsActive { get; set; }
    public bool IsClientPortalActive { get; set; }
    public bool IsEnabledForComanaged { get; set; }
    public bool IsTaskFireActive { get; set; }
    public bool IsTaxExempt { get; set; }
    public DateTime LastActivityDate { get; set; }
    public DateTime LastTrackedModifiedDateTime { get; set; }
    public int? MarketSegmentID { get; set; }
    public required int OwnerResourceID { get; set; }
    public int? ParentCompanyID { get; set; }
    [MaxLength(25)] public required string Phone { get; set; }
    [MaxLength(30)] public string PostalCode { get; set; }
    public int? PurchaseOrderTemplateID { get; set; }
    public int QuoteEmailMessageID { get; set; }
    public int QuoteTemplateID { get; set; }
    [MaxLength(32)] public string SicCode { get; set; }
    [MaxLength(50)] public string State { get; set; }
    [MaxLength(10)] public string StockMarket { get; set; }
    [MaxLength(10)] public string StockSymbol { get; set; }
    public double? SurveyCompanyRating { get; set; }
    [MaxLength(50)] public string TaxID { get; set; }
    public int? TaxRegionID { get; set; }
    public int? TerritoryID { get; set; }
    [MaxLength(255)] public string WebAddress { get; set; }

    [MaxLength(200)]
    public IEnumerable<UserDefinedField> UserDefinedFields { get; set; } = new List<UserDefinedField>();
}