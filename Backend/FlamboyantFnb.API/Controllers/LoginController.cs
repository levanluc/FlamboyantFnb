using FlamboyantFnb.Controllers;
using FlamboyantFnb.Domain.Interfaces.Services;
using FlamboyantFnb.Domain.RequestModel;
using FlamboyantFnb.Domain.Response;
using FlamboyantFnb.Helper;
using Microsoft.AspNetCore.Mvc;

namespace FlamboyantFnb.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, IUserService userService) : base(configuration)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<BaseResponse<LoginResponse>> Post(LoginReq req)
        {
            var res = await _userService.LoginAsync(req);
            Response.Cookies.Append(Constant.SessionCookieHeader, res.SessionId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(AppServiceConfig.DayDuration)
            });
            return Ok(res);
        }
    }
}
