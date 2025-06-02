using FlamboyantFnb.Domain.Exceptions;
using FlamboyantFnb.Domain.Response;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using FlamboyantFnb.Helper;
using FlamboyantFnb.Infrastructure.Repository;
using System.Net;
using StackExchange.Redis;
using FlamboyantFnb.Domain.Interfaces.Cache;
using FlamboyantFnb.Infrastructure.Cache;
using FlamboyantFnb.Domain.Context;
using FlamboyantFnb.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.Interfaces.Services;
using FlamboyantFnb.Services;
using Serilog;

namespace FlamboyantFnb.Extensions
{
    public static class GlobalExtentions
    {
        public static void UseGlobalExceptionProcess(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async (httpContext) =>
            {
                var exceptionFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionFeature?.Error;

                var resp = new BaseResponse<object>();
                resp.Code = -1;

                if (exception is ValidateException)
                {
                    Log.Information("Validation error: {Message}", exception?.Message);
                    resp.Message = exception?.Message;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else if (exception is UnauthorizedAccessException || exception is SecurityTokenExpiredException)
                {
                    resp.Message = exception?.Message;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    Log.Error(exception, exception?.Message!);
                    resp.Message = "Something went wrong. Please try again!";
                }
                var jsonRes = JsonSerializer.Serialize(resp);
                await httpContext.Response.WriteAsync(jsonRes);
            });
        }

        public static void LoadSecretKey(this IHostApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            var jwtSection = configuration.GetSection("Jwt");
            if (jwtSection != null)
            {
                var privateKey = File.ReadAllText(jwtSection?.GetSection("PrivateKeyPath")?.Value ?? string.Empty);
                if (!string.IsNullOrEmpty(privateKey))
                {
                    configuration["Jwt:PrivateKey"] = privateKey;
                }
                var publicKey = File.ReadAllText(jwtSection?.GetSection("PublicKeyPath")?.Value ?? string.Empty);
                if (!string.IsNullOrEmpty(publicKey))
                {
                    configuration["Jwt:PublicKey"] = publicKey;
                }
            }
        }

        public static void ConfigServices(this IHostApplicationBuilder builder)
        {
            #region repository
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            var connectionStr = builder.Configuration.GetConnectionString("FlamboyantEntity");
            builder.Services.AddDbContext<FnbDbContext>(options => options.UseSqlServer(connectionStr));
            #endregion

            #region database
            #endregion
        }

        public static void AddRedis(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                return ConnectionMultiplexer.Connect(AppServiceConfig.RedisConnectionString);
            });

            builder.Services.AddScoped<ICacheClient, CacheClient>();
        }

        public static void AddContext(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(sp =>
            {
                var nonExistentUser = new FnbExecutionContext()
                {
                    SessionId = string.Empty,
                    FullName = string.Empty,
                    UserName = string.Empty
                };
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var context = httpContextAccessor.HttpContext!;
                var sessionId = context.Request.Cookies[Constant.SessionCookieHeader];
                if (string.IsNullOrEmpty(sessionId)) return nonExistentUser;

                var cacheClient = context.RequestServices.GetService<ICacheClient>()!;
                var sessionKey = string.Format(RedisKeys.UserSessionKey, sessionId);
                var sessionCache = cacheClient.GetString(sessionKey);
                if (string.IsNullOrEmpty(sessionCache)) return nonExistentUser;

                var user = JsonSerializer.Deserialize<User>(sessionCache)!;
                return new FnbExecutionContext()
                {
                    SessionId = sessionId,
                    UserId = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName
                };
            });
        }
    }
}
