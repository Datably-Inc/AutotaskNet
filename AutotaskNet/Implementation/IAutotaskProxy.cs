using System.Text.Json;
using AutotaskNet.Domain;
using AutotaskNet.Domain.AttachmentChildEntities;
using AutotaskNet.Domain.ChildEntities;
using AutotaskNet.Domain.Requests;
using AutotaskNet.Domain.Requests.Queries;
using AutotaskNet.Domain.Responses;
using AutotaskNet.Domain.RootEntities;
using AutotaskNet.Utilities;

namespace AutotaskNet.Implementation;

internal interface IAutotaskProxy : IDisposable
{
    Task<ItemIdResult> CreateAsync<T>(string endpoint, T entity) where T : AutotaskEntity;

    Task<ItemIdResult> UpdateAsync<T>(string endpoint, T entity) where T : AutotaskEntity;

    Task<ItemIdResult> DeleteAsync<T>(string endpoint, long id) where T : AutotaskEntity;

    Task<IEnumerable<T>> QueryAsync<T>(string endpoint, QueryFilter filter, int pageCount = int.MaxValue)
        where T : AutotaskRootEntity;

    Task<IEnumerable<T>> QueryAsync<T>(string endpoint, int pageCount = int.MaxValue) where T : AutotaskChildEntity;

    Task<int> QueryCountAsync<T>(string endpoint, QueryFilter filter) where T : AutotaskRootEntity;

    Task<T> GetAsync<T>(string endpoint, long id) where T : AutotaskEntity;

    Task<T> GetAttachmentAsync<T>(string endpoint, long id) where T : AutotaskAttachmentChildEntity;

    Task<EntityInformationResult> GetEntityInformationAsync<T>(string endpoint) where T : AutotaskEntity;

    Task<EntityFieldsResult> GetEntityFieldsAsync<T>(string endpoint) where T : AutotaskEntity;

    Task<EntityUserDefinedFieldsResult> GetEntityUserDefinedFieldsAsync<T>(string endpoint) where T : AutotaskEntity;

    internal class Imp : IAutotaskProxy
    {
        private readonly IProxy _proxy;
        private readonly AutotaskCredentials _credentials;
        private readonly ZoneUriGetter _zoneUriGetter;

        public Imp(IProxy proxy, AutotaskCredentials credentials)
        {
            _proxy = proxy;
            _credentials = credentials;
            _zoneUriGetter = new ZoneUriGetter(proxy, credentials);
        }

        public Task<ItemIdResult> CreateAsync<T>(string endpoint, T entity) where T : AutotaskEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint);

            var message = CreateHttpRequestMessage(HttpMethod.Post, uri);
            return _proxy.SendAsync<ItemIdResult>(message, entity);
        }

        public Task<ItemIdResult> UpdateAsync<T>(string endpoint, T entity) where T : AutotaskEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint);

            var message = CreateHttpRequestMessage(HttpMethod.Put, uri);
            return _proxy.SendAsync<ItemIdResult>(message, entity);
        }

        public Task<ItemIdResult> DeleteAsync<T>(string endpoint, long id) where T : AutotaskEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath(id);

            var message = CreateHttpRequestMessage(HttpMethod.Delete, uri);
            return _proxy.SendAsync<ItemIdResult>(message);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string endpoint, QueryFilter filter, int pageCount = int.MaxValue)
            where T : AutotaskRootEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath("query")
                .UpsertQueryParam("search", JsonSerializer.Serialize(filter));

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            return QueryPagesAsync<T>(message, pageCount);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string endpoint, int pageCount = int.MaxValue)
            where T : AutotaskChildEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint);

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            return QueryPagesAsync<T>(message, pageCount);
        }

        public async Task<int> QueryCountAsync<T>(string endpoint, QueryFilter filter) where T : AutotaskRootEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath("query")
                .UpsertQueryParam("search", JsonSerializer.Serialize(filter));

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await _proxy.SendAsync<QueryCountResult>(message);
            return result.QueryCount;
        }

        public async Task<T> GetAsync<T>(string endpoint, long id) where T : AutotaskEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath(id);

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await _proxy.SendAsync<SingleResult<T>>(message);
            return result.Item;
        }

        public async Task<T> GetAttachmentAsync<T>(string endpoint, long id) where T : AutotaskAttachmentChildEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath(id);

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            var result = await _proxy.SendAsync<PaginatedResult<T>>(message);
            return result.Items.First();
        }

        public Task<EntityInformationResult> GetEntityInformationAsync<T>(string endpoint) where T : AutotaskEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath("entityInformation");

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            return _proxy.SendAsync<EntityInformationResult>(message);
        }

        public Task<EntityFieldsResult> GetEntityFieldsAsync<T>(string endpoint) where T : AutotaskEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath("entityInformation")
                .AppendRelativePath("fields");

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            return _proxy.SendAsync<EntityFieldsResult>(message);
        }

        public Task<EntityUserDefinedFieldsResult> GetEntityUserDefinedFieldsAsync<T>(string endpoint)
            where T : AutotaskEntity
        {
            var uri = _zoneUriGetter.Uri
                .AppendRelativePath(endpoint)
                .AppendRelativePath("entityInformation")
                .AppendRelativePath("userDefinedFields");

            var message = CreateHttpRequestMessage(HttpMethod.Get, uri);
            return _proxy.SendAsync<EntityUserDefinedFieldsResult>(message);
        }

        private HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, Uri url)
        {
            var message = new HttpRequestMessage(method, url);

            message.Headers.Add(nameof(AutotaskCredentials.ApiIntegrationCode), _credentials.ApiIntegrationCode);
            message.Headers.Add(nameof(AutotaskCredentials.UserName), _credentials.UserName);
            message.Headers.Add(nameof(AutotaskCredentials.Secret), _credentials.Secret);

            return message;
        }

        private async Task<IEnumerable<T>> QueryPagesAsync<T>(HttpRequestMessage message, int pageCount = int.MaxValue)
        {
            var currentResult = await _proxy.SendAsync<PaginatedResult<T>>(message);

            var results = new List<T>();
            results.AddRange(currentResult.Items);

            var currentPage = 1;
            while (currentResult.PageDetails.NextPageUrl != null && currentPage < pageCount)
            {
                message = CreateHttpRequestMessage(HttpMethod.Get, new Uri(currentResult.PageDetails.NextPageUrl));
                currentResult = await _proxy.SendAsync<PaginatedResult<T>>(message);
                results.AddRange(currentResult.Items);

                currentPage++;
            }

            return results;
        }

        public void Dispose() => _proxy.Dispose();
    }

    public class ZoneUriGetter
    {
        private readonly string _apiVersion = "v1.0";

        private readonly IProxy _proxy;
        private readonly AutotaskCredentials _credentials;

        private Uri? _uri;
        public Uri Uri => _uri ??= GetZoneUriAsync().Result;

        public ZoneUriGetter(IProxy proxy, AutotaskCredentials credentials)
        {
            _proxy = proxy;
            _credentials = credentials;
        }

        private async Task<Uri> GetZoneUriAsync()
        {
            var endpoint = new Uri($"https://webservices.autotask.net/atservicesrest/{_apiVersion}")
                .AppendRelativePath("zoneInformation")
                .UpsertQueryParam("user", _credentials.UserName);

            var message = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var parsed = await _proxy.SendAsync<ZoneInformation>(message);

            return new Uri(parsed.Url)
                .AppendRelativePath(_apiVersion);
        }
    }
}