using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Npgsql;
using SSRUnited.Shared.Common;
using System.Net;
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

        public async Task<Result<TResponse>?> GetAsync<TResponse>(string url, CancellationToken cancellationToken = default!)
        {
            var response = await _http.GetAsync(url);
            return await MapStatusCodeAsync<TResponse>(response);

        }
        public async Task<Result<TResponse>?> UpdateAsync<TRequest, TResponse>(string url, TRequest request, CancellationToken cancellationToken = default!)
        {
            var response = await _http.PutAsJsonAsync(url, request);
            return await MapStatusCodeAsync<TResponse>(response);

        }

        public async Task<Result<TResponse>?> DeleteAsync<TResponse>(string url, CancellationToken cancellationToken = default!)
        {
            var response = await _http.DeleteAsync(url);
            return await MapStatusCodeAsync<TResponse>(response);

        }

        public async Task<Result<TResponse>> MapStatusCodeAsync<TResponse>(HttpResponseMessage response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => await HandleOkAsync<TResponse>(response),
                HttpStatusCode.Created => await HandleOkAsync<TResponse>(response),
                HttpStatusCode.NoContent => Result<TResponse>.NoContent(),
                HttpStatusCode.NotFound => Result<TResponse>.NotFound(),
                HttpStatusCode.Forbidden => Result<TResponse>.Forbidden(),
                HttpStatusCode.Unauthorized => Result<TResponse>.Unauthorized(),
                HttpStatusCode.Conflict => Result<TResponse>.Conflict(),
                _ => Result<TResponse>.BadRequest()
            };
        }
        public async Task<Result<TResponse>> HandleOkAsync<TResponse>(HttpResponseMessage response)
        {
            var value = await response.Content.ReadFromJsonAsync<TResponse>();
            if (value is null) return Result<TResponse>.Failure("Failed to deserialize response");
            return Result<TResponse>.Success(value);
        }

    }
}
