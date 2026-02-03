using SSRUnited.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSRUnited.Shared.Interface
{
    public interface IIRespository
    {
        Task CreateAsync(HumanDto request, CancellationToken cancellationToken = default!);
        Task<List<HumanDto>> Listing(CancellationToken cancellationToken  = default!);
        Task<HumanDto?> Get(int Id, CancellationToken cancellationToken = default!);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default!);
        Task<bool> Update(HumanDto request, CancellationToken cancellationToken = default!);

    }
}
