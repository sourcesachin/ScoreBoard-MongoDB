using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace LeaderboardMicroServices.Controllers
{

    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet,Route("api")]
        public String Api() {
            return "Welcome to ScoreBoard!";
        }

        [HttpGet]
        [Route("generateTokken/{username}")]
        public String generateTokken(String username)
        {
            return this.generateJwtToken(username);
        }

        private string generateJwtToken(String username)
        {
            var tokkensecret = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImp0aSI6Ijg5NTBiMWIwLTk0ZTQtNDhjOC1hYWI4LWNhYzU0ZDc3MGZkYyIsImlhdCI6MTYwMDYwMTg4NywiZXhwIjoxNjAwNjA1NDg3fQ.dQMvYXlD-nqFZFy8hbeeR15riWWwH1ai7rhyXLDdR7g";
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //configuration.GetValue<string>("Jwt:Secret"); 
            var key = Encoding.ASCII.GetBytes(tokkensecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}