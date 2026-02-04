using SSRUnited.Shared.Dtos;

namespace SSRUnited.Client.Interface
{
    public interface ICrudApiClient
    {
        Task CreateAsync(HumanDto request, CancellationToken cancellationToken = default!);
        Task<HumanDto?> Get(int Id, CancellationToken cancellationToken = default!);
        Task<List<HumanDto>?> Listing(CancellationToken cancellationToken = default!);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default!);
        Task<bool> Update(HumanDto request, CancellationToken cancellationToken = default!);
    }
}
