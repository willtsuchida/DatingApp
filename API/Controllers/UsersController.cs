using API.Controller;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
// [ApiController]    - Passamos herdar da BaseApiController
// [Route("api/[controller]")] // https://localhost:5001/api/users/[http-method]- Passamos herdar da BaseApiController

[Authorize] //APlica pra todos metodos
public class UsersController : BaseApiController
{
    private readonly DataContext _context; //Pode ser usado em toda a classe

    public UsersController(DataContext context) //injeta o DataContext qnd o UserController eh criado eh SCOPED...
    {
        _context = context;
    }

    //API ENDPOINTS
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    // [Authorize] //SO quem tem token valido pode ver essa parte
    [HttpGet("{id}")] //https://localhost:5001/api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
