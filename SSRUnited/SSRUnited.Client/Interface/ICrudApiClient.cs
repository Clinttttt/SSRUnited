using SSRUnited.Shared.Common;
using SSRUnited.Shared.Dtos;

namespace SSRUnited.Client.Interface
{
    public interface ICrudApiClient
    {
        Task CreateAsync(HumanDto request, CancellationToken cancellationToken = default!);
        Task<Result<HumanDto>?> Get(int Id, CancellationToken cancellationToken = default!);
        Task<Result<List<HumanDto>>?> Listing(CancellationToken cancellationToken = default!);
        Task<Result<bool>?> Delete(int Id, CancellationToken cancellationToken = default!);
        Task<Result<bool>?> Update(HumanDto request, CancellationToken cancellationToken = default!);
    }
}
