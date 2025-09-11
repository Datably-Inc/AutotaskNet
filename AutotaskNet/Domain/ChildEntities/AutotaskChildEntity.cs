namespace AutotaskNet.Domain.ChildEntities;

public abstract class AutotaskChildEntity : AutotaskEntity
{
    protected abstract string ParentEndpoint { get; }

    public string ChildEndpoint(long parentId)
    {
        return $"{ParentEndpoint}/{parentId}/{Endpoint}";
    }
}