using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSRUnited.Shared.Common;

namespace SSRUnited.Api.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {


        public ActionResult<T> HandleResponse<T>(Result<T> result )
        {
            if (result.is_success)         
                return Ok(result.value);
            return result.status_code switch
            {
                404 => NotFound(),
                401 => Unauthorized(),
                403 => Forbid(),
                409 => Conflict(),
                204 => NoContent(),
                500 => StatusCode(500, "Internal Server Error"),
                _ => BadRequest()
            };
            
        }
        public ActionResult HandleNoResponse() => Ok();      
      
    }
}
