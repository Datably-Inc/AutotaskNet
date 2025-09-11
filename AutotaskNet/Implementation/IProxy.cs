using System.Text;
using System.Text.Json;
using AutotaskNet.Utilities;

namespace AutotaskNet.Implementation;

internal interface IProxy
{
    Task<T> SendAsync<T>(HttpRequestMessage message);
    Task<T> SendAsync<T>(HttpRequestMessage message, object payload);

    internal class Imp : IProxy
    {
        private readonly HttpClient _client;

        public Imp(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage message)
        {
            var result = await SendAndAssertSuccessAsync(message);
            return await DeserializeResponseAsync<T>(result);
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage message, object payload)
        {
            message.Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var result = await SendAndAssertSuccessAsync(message);
            return await DeserializeResponseAsync<T>(result);
        }

        private async Task<HttpResponseMessage> SendAndAssertSuccessAsync(HttpRequestMessage message)
        {
            var result = await _client.SendAsync(message)
                         ?? throw new AutotaskNetCoreException(
                             $"Autotask response was null for request: {message.RequestUri}");

            if (result.IsSuccessStatusCode)
                return result;

            var json = await result.Content.ReadAsStringAsync();
            throw new AutotaskNetCoreException(
                $"Autotask response was not successful for request: {message.RequestUri}\n" +
                $"Response: {json}");
        }

        private async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var parsed = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return parsed
                   ?? throw new AutotaskNetCoreException(
                       $"Unable to parse Autotask response into {typeof(T)} " +
                       $"for request: {response.RequestMessage.RequestUri}\n" +
                       $"Autotask response: {content}");
        }
    }
}