using SSRUnited.Client.Helper;
using SSRUnited.Client.Interface;
using SSRUnited.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace SSRUnited.Client.Services
{
    public class CrudApiClient : HandleResponse, ICrudApiClient
    {
        public CrudApiClient(HttpClient http) : base(http)
        {
        }

        public async Task CreateAsync(HumanDto request, CancellationToken cancellationToken = default!)
            => await PostAsync("api/Crud/Create", request,cancellationToken);

        public async Task<HumanDto?> Get(int Id, CancellationToken cancellationToken = default!)
          => await GetAsync<HumanDto>($"api/Crud/Get{Id}", cancellationToken);

        public async Task<List<HumanDto>?> Listing(CancellationToken cancellationToken = default!)
            => await GetAsync<List<HumanDto>>("api/Crud/Listing", cancellationToken);

        public async Task<bool> Delete(int Id, CancellationToken cancellationToken = default!)
           => await DeleteAsync<bool>($"api/Crud/Delete/{Id}", cancellationToken);

        public async Task<bool> Update(HumanDto request, CancellationToken cancellationToken = default!)
                => await UpdateAsync<HumanDto,bool>("api/Crud/Update", request, cancellationToken);
        
    }

}
