using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key; //2 types of key, simetrica (so fica no server) - usa mesma key pra cripto e descripto -- Asimetrica usa qnd o server e client precisa criptografar algo

    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); //key:value
    }

    public string CreateToken(AppUser user)
    {
        //claims
        var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
        };

        //sign
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        //describe token return
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
