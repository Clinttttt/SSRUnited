using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace SSRUnited.Client.Helper
{
    public abstract class HandleResponse
    {
        private readonly HttpClient _http;
        public HandleResponse(HttpClient http)
        {
            _http = http;
        }

        public async Task PostAsync<TRequest>(string url, TRequest request, CancellationToken cancellationToken = default!)
        {
            var response = await _http.PostAsJsonAsync(url, request);
            response.EnsureSuccessStatusCode();

        }

        public async Task<TResponse?> GetAsync<TResponse>(string url, CancellationToken cancellationToken = default!)
        {
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);

        }
        public async Task<TResponse?> UpdateAsync<TRequest, TResponse>(string url, TRequest request, CancellationToken cancellationToken = default!)
        {
            var response = await _http.PutAsJsonAsync(url, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);

        }

        public async Task<TResponse?> DeleteAsync<TResponse>(string url, CancellationToken cancellationToken = default!)
        {
            var response = await _http.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);

        }


    }
}
