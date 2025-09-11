namespace AutotaskNet.Domain.Responses;

public class EntityInformationResult
{
    public EntityInformation Info { get; set; }

    public class EntityInformation
    {
        public string Name { get; set; }
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanQuery { get; set; }
        public bool CanUpdate { get; set; }
        public string UserAccessForCreate { get; set; }
        public string UserAccessForDelete { get; set; }
        public string UserAccessForQuery { get; set; }
        public string UserAccessForUpdate { get; set; }
        public bool HasUserDefinedFields { get; set; }
        public bool SupportsWebhookCallouts { get; set; }
    }
}