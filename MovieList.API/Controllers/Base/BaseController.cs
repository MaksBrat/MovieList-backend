using Microsoft.AspNetCore.Mvc;
using MovieList.Attributes;
using MovieList.Common.Extentions;

namespace MovieList.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected int UserId => GetUserId();

        private int GetUserId() => HttpContext.User?.GetUserId() ?? 0;
    }
}
