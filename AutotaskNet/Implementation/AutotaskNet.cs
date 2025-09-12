using AutotaskNet.Api;
using AutotaskNet.Domain;
using AutotaskNet.Domain.AttachmentChildEntities;
using AutotaskNet.Domain.ChildEntities;
using AutotaskNet.Domain.Requests;
using AutotaskNet.Domain.Requests.Queries;
using AutotaskNet.Domain.Responses;
using AutotaskNet.Domain.RootEntities;

namespace AutotaskNet.Implementation;

internal class AutotaskNet : IAutotaskNet
{
    private readonly IAutotaskProxy _autotaskProxy;

    public AutotaskNet(IAutotaskProxy autotaskProxy)
    {
        _autotaskProxy = autotaskProxy;
    }

    #region Root Entity Endpoints

    public Task<ItemIdResult> CreateAsync<T>(T rootEntity) where T : AutotaskRootEntity
    {
        rootEntity.AssertValidStateAndOperation(AutotaskOperation.Create);

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.CreateAsync(endpoint, rootEntity);
    }

    public Task<ItemIdResult> UpdateAsync<T>(T rootEntity) where T : AutotaskRootEntity
    {
        rootEntity.AssertValidStateAndOperation(AutotaskOperation.Update);

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.UpdateAsync(endpoint, rootEntity);
    }

    public Task<ItemIdResult> DeleteAsync<T>(long rootEntityId) where T : AutotaskRootEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Delete);

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.DeleteAsync<T>(endpoint, rootEntityId);
    }

    public Task<IEnumerable<T>> QueryAsync<T>(QueryFilter filter, int pageCount = int.MaxValue)
        where T : AutotaskRootEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Query);
        filter.AssertValidState();

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.QueryAsync<T>(endpoint, filter, pageCount);
    }

    public Task<int> QueryCountAsync<T>(QueryFilter filter) where T : AutotaskRootEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Query);
        filter.AssertValidState();

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.QueryCountAsync<T>(endpoint, filter);
    }

    public Task<T> GetAsync<T>(long rootEntityId) where T : AutotaskRootEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Query);

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.GetAsync<T>(endpoint, rootEntityId);
    }

    public Task<EntityInformationResult> GetEntityInformationAsync<T>() where T : AutotaskRootEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.GetEntityInformation);

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.GetEntityInformationAsync<T>(endpoint);
    }

    public Task<EntityFieldsResult> GetEntityFieldsAsync<T>() where T : AutotaskRootEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.GetEntityInformation);

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.GetEntityFieldsAsync<T>(endpoint);
    }

    public Task<EntityUserDefinedFieldsResult> GetEntityUserDefinedFieldsAsync<T>() where T : AutotaskRootEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.GetEntityInformation);

        var endpoint = AutotaskEndpoints.GetEndpoint<T>();
        return _autotaskProxy.GetEntityUserDefinedFieldsAsync<T>(endpoint);
    }

    #endregion

    #region Child Entity Endpoints

    public Task<ItemIdResult> CreateAsync<T>(long parentId, T childEntity) where T : AutotaskChildEntity
    {
        childEntity.AssertValidStateAndOperation(AutotaskOperation.Create);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.CreateAsync(endpoint, childEntity);
    }

    public Task<ItemIdResult> UpdateAsync<T>(long parentId, T childEntity) where T : AutotaskChildEntity
    {
        childEntity.AssertValidStateAndOperation(AutotaskOperation.Update);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.UpdateAsync(endpoint, childEntity);
    }

    public Task<ItemIdResult> DeleteAsync<T>(long parentId, long childId) where T : AutotaskChildEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Delete);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.DeleteAsync<T>(endpoint, childId);
    }

    public Task<IEnumerable<T>> QueryAsync<T>(long parentId, int pageCount = int.MaxValue) where T : AutotaskChildEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Query);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.QueryAsync<T>(endpoint, pageCount);
    }

    public Task<T> GetAsync<T>(long parentId, long childId) where T : AutotaskChildEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Query);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.GetAsync<T>(endpoint, childId);
    }

    public Task<EntityInformationResult> GetEntityInformationAsync<T>(long parentId) where T : AutotaskChildEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.GetEntityInformation);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.GetEntityInformationAsync<T>(endpoint);
    }

    public Task<EntityFieldsResult> GetEntityFieldsAsync<T>(long parentId) where T : AutotaskChildEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.GetEntityInformation);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.GetEntityFieldsAsync<T>(endpoint);
    }

    public Task<EntityUserDefinedFieldsResult> GetEntityUserDefinedFieldsAsync<T>(long parentId)
        where T : AutotaskChildEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.GetEntityInformation);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.GetEntityUserDefinedFieldsAsync<T>(endpoint);
    }

    #endregion

    #region Attachment Child Entity Endpoints

    public Task<T> GetAttachmentDataAsync<T>(long parentId, long attachmentId) where T : AutotaskAttachmentChildEntity
    {
        AssertGenericOperation<T>(AutotaskOperation.Query);

        var endpoint = AutotaskEndpoints.GetChildEndpoint<T>(parentId);
        return _autotaskProxy.GetAttachmentAsync<T>(endpoint, attachmentId);
    }

    #endregion

    private void AssertGenericOperation<T>(AutotaskOperation operation) where T : AutotaskEntity
    {
        var instance = (T)Activator.CreateInstance(typeof(T));
        instance.AssertValidOperation(operation);
    }
}