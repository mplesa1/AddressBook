using AddressBook.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AddressBook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {

        protected IActionResult ApiResponseOk(object response)
        {
            return Ok(ApiResponse.CreateResponse(HttpStatusCode.OK, response));
        }

        protected IActionResult ApiResponseOk()
        {
            return Ok(ApiResponse.CreateResponse(HttpStatusCode.OK ,null));
        }

        protected IActionResult ApiResponseBadRequest()
        {
            return Ok(ApiResponse.CreateResponse(HttpStatusCode.BadRequest, null));
        }
    }
}
