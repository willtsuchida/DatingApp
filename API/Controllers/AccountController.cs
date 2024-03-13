using System.Security.Cryptography;
using System.Text;
using API.Controller;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("register")] //POST: api/accounts/register
    // POST: api/accounts/register?username=Will&password=pwd <-- query parameter, autobind com os parametros do metodo (nao vai pelo body) - nao usar
    // Passar pelo Body!
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        //Hash the password
        using var hmac = new HMACSHA512(); //Using --> quando cria uma nova classe (new) consome espaço, ao finalizar dar dispose automatico com o using.
        //hash-based mesasage
        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), //PasswordHash eh do tipo bit[], o metodo ComputeHash do hmac retorna um array de bit
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) //Para retornar Ok, Unauthorezed, BadRequest, etc tem que usar ActionResult
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username);
        if (user == null) return Unauthorized("Invalid Username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid password");
            }
        }

        return Ok(new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        });
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
    }

}
