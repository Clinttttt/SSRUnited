using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSRUnited.Shared.Dtos;
using SSRUnited.Shared.Interface;
using System.Runtime.InteropServices;

namespace SSRUnited.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController(IIRespository respository) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync([FromBody] HumanDto request, CancellationToken cancellationToken = default!)
        {
            await respository.CreateAsync(request, cancellationToken);
            return Ok();
        }
        [HttpGet("Listing")]
        public async Task<ActionResult<List<HumanDto>>> Listing(CancellationToken cancellationToken)
        {
            var command = await respository.Listing(cancellationToken);
            return Ok(command);
        }
        [HttpGet("Get/{Id}")]
        public async Task<ActionResult<HumanDto?>> Get([FromRoute] int Id, CancellationToken cancellationToken = default!)
        {
            var command = await respository.Get(Id, cancellationToken);
            return Ok(command);
        }
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int Id, CancellationToken cancellationToken = default!)
        {
            var command = await respository.Delete(Id, cancellationToken);
            return Ok(command);
        }
        [HttpPatch("Update")]
        public async Task<ActionResult<bool>> Update([FromBody] HumanDto request, CancellationToken cancellationToken = default!)
        {
            var command = await respository.Update(request, cancellationToken);
            return Ok(command);
        }

    }
}
