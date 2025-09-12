using AutotaskNet.Domain.AttachmentChildEntities;
using AutotaskNet.Domain.ChildEntities;
using AutotaskNet.Domain.Requests.Queries;
using AutotaskNet.Domain.Responses;
using AutotaskNet.Domain.RootEntities;

namespace AutotaskNet.Api;

/// <summary>
/// Abstracts the Autotask REST API.
/// </summary>
public interface IAutotaskNet
{
    #region Root Entity Endpoints

    /// <summary>
    /// Creates a root entity.
    /// </summary>
    Task<ItemIdResult> CreateAsync<T>(T rootEntity) where T : AutotaskRootEntity;

    /// <summary>
    /// Updates a root entity.
    /// </summary>
    Task<ItemIdResult> UpdateAsync<T>(T rootEntity) where T : AutotaskRootEntity;

    /// <summary>
    /// Deletes a root entity.
    /// </summary>
    Task<ItemIdResult> DeleteAsync<T>(long rootEntityId) where T : AutotaskRootEntity;

    /// <summary>
    /// Returns all root entities that match the filter.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="pageCount">The number of pages to return. A page is 500 entities.</param>
    Task<IEnumerable<T>> QueryAsync<T>(QueryFilter filter, int pageCount = int.MaxValue) where T : AutotaskRootEntity;

    /// <summary>
    /// Returns the count of root entities that match the filter.
    /// </summary>
    Task<int> QueryCountAsync<T>(QueryFilter filter) where T : AutotaskRootEntity;

    /// <summary>
    /// Returns a root entity that matches the Id.
    /// </summary>
    Task<T> GetAsync<T>(long rootEntityId) where T : AutotaskRootEntity;

    /// <summary>
    /// Retrieves information about a root entity.
    /// </summary>
    Task<EntityInformationResult> GetEntityInformationAsync<T>() where T : AutotaskRootEntity;

    /// <summary>
    /// Retrieves information about a root entity's fields.
    /// </summary>
    Task<EntityFieldsResult> GetEntityFieldsAsync<T>() where T : AutotaskRootEntity;

    /// <summary>
    /// Retrieves information about a root entity's user defined fields.
    /// </summary>
    Task<EntityUserDefinedFieldsResult> GetEntityUserDefinedFieldsAsync<T>() where T : AutotaskRootEntity;

    #endregion

    #region Child Entity Endpoints

    /// <summary>
    /// Creates a child entity.
    /// </summary>
    Task<ItemIdResult> CreateAsync<T>(long parentId, T childEntity) where T : AutotaskChildEntity;

    /// <summary>
    /// Updates a child entity.
    /// </summary>
    Task<ItemIdResult> UpdateAsync<T>(long parentId, T childEntity) where T : AutotaskChildEntity;

    /// <summary>
    /// Deletes a child entity.
    /// </summary>
    Task<ItemIdResult> DeleteAsync<T>(long parentId, long childId) where T : AutotaskChildEntity;

    /// <summary>
    /// Returns all child entities for the parent.
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="pageCount">The number of pages to return. A page is 500 entities.</param>
    Task<IEnumerable<T>> QueryAsync<T>(long parentId, int pageCount = int.MaxValue) where T : AutotaskChildEntity;

    /// <summary>
    /// Returns a child entity that matches the Id.
    /// </summary>
    Task<T> GetAsync<T>(long parentId, long childId) where T : AutotaskChildEntity;

    /// <summary>
    /// Retrieves information about a child entity.
    /// </summary>
    Task<EntityInformationResult> GetEntityInformationAsync<T>(long parentId) where T : AutotaskChildEntity;

    /// <summary>
    /// Retrieves information about a child entity's fields.
    /// </summary>
    Task<EntityFieldsResult> GetEntityFieldsAsync<T>(long parentId) where T : AutotaskChildEntity;

    /// <summary>
    /// Retrieves information about a child entity's user defined fields.
    /// </summary>
    Task<EntityUserDefinedFieldsResult> GetEntityUserDefinedFieldsAsync<T>(long parentId) where T : AutotaskChildEntity;

    #endregion

    #region Attachment Child Entity Endpoints

    /// <summary>
    /// Returns the attachment entity along with its file data.
    /// </summary>
    Task<T> GetAttachmentDataAsync<T>(long parentId, long attachmentId) where T : AutotaskAttachmentChildEntity;

    #endregion
}