using FlamboyantFnb.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace FlamboyantFnb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration _configuration;

        public BaseController(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        protected BaseResponse<T> Ok<T>(T res)
        {
            var rs = new BaseResponse<T>();
            rs.Data = res;
            return rs;
        }
    }
}
