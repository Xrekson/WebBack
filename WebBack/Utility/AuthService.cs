using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotenv.net;
using Microsoft.IdentityModel.Tokens;
using WebBack.Model;

namespace WebBack.Utility;

public class AuthService
{
    public Auth GenerateToken(Users user)
    {
        var envVars = DotEnv.Read();
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(envVars["JWT_TOKEN"]);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);
            
        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.MobilePhone, user.Mobile),
        new Claim("Id", user.Id.ToString())
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        var stringToken = handler.WriteToken(token);
        return new Auth(user.Id,stringToken,tokenDescriptor.Expires);
    }

    private static ClaimsIdentity GenerateClaims(Users user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        return claims;
    }
    public record Auth(int id,string token,DateTime? expiresIn);
}
