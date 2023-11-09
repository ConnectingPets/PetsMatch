using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    // [Route("[controller]")]
    // public class BaseApiController : Controller
    // {
    //     private readonly ILogger<BaseApiController> _logger;

    //     public BaseApiController(ILogger<BaseApiController> logger)
    //     {
    //         _logger = logger;
    //     }

    //     public IActionResult Index()
    //     {
    //         return View();
    //     }

    //     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //     public IActionResult Error()
    //     {
    //         return View("Error!");
    //     }
    // }

    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>();

      /*  protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }*/

        //TODO: Check if we are going to need this pagedRes.
        // protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        // {
        //     if (result == null) return NotFound();
        //     if (result.IsSuccess && result.Value != null)
        //     {
        //         Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize,
        //             result.Value.TotalCount, result.Value.TotalPages);
        //         return Ok(result.Value);
        //     }

        //     if (result.IsSuccess && result.Value == null)
        //         return NotFound();
        //     return BadRequest(result.Error);
        // }
    }
}