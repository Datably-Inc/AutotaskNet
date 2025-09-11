using AutotaskNet.Domain.ChildEntities;
using AutotaskNet.Domain.RootEntities;

namespace AutotaskNet.Domain.Requests;

internal static class AutotaskEndpoints
{
    public static string GetEndpoint<T>() where T : AutotaskRootEntity
    {
        var instance = (T)Activator.CreateInstance(typeof(T));
        return instance.Endpoint;
    }

    public static string GetChildEndpoint<T>(long parentId) where T : AutotaskChildEntity
    {
        var instance = (T)Activator.CreateInstance(typeof(T));
        return instance.ChildEndpoint(parentId);
    }
}