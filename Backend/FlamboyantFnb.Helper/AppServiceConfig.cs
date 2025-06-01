using Microsoft.Extensions.Configuration;
using System;

namespace FlamboyantFnb.Helper
{
    public static class AppServiceConfig
    {
        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private static IConfiguration Configuration { get; set; }
        public static string PrivateKey => Configuration?.GetSection("Jwt:PrivateKey")?.Value;
        public static string PublicKey => Configuration?.GetSection("Jwt:PublicKey")?.Value;
        public static double DayDuration => Convert.ToDouble(Configuration?.GetSection("Jwt:DayDuration")?.Value);
        public static string Issuer => Configuration?.GetSection("Jwt:Issuer")?.Value;
        public static string Audience => Configuration?.GetSection("Jwt:Audience")?.Value;

        public static string RedisConnectionString => Configuration?.GetSection("Redis:ConnectionString")?.Value;
        public static string FrontEndOrigin => Configuration?.GetSection("FrontEndOrigin")?.Value;
    }
}
