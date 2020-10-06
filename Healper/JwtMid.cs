using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardMicroServices.Helper
{
    public class JwtMid
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public JwtMid(IConfiguration configuration, RequestDelegate next)
        {
            _next = next; _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context,token);

            await _next(context);
        }
        private void attachUserToContext(HttpContext context, string token)
        {
            var tokkensecret = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImp0aSI6Ijg5NTBiMWIwLTk0ZTQtNDhjOC1hYWI4LWNhYzU0ZDc3MGZkYyIsImlhdCI6MTYwMDYwMTg4NywiZXhwIjoxNjAwNjA1NDg3fQ.dQMvYXlD-nqFZFy8hbeeR15riWWwH1ai7rhyXLDdR7g";
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(tokkensecret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var xusername = jwtToken.Claims.First(x => x.Type == "username").Value;
                // attach user to context on successful jwt validation
                context.Items["User"] = xusername;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
