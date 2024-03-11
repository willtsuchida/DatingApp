using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")] // https://localhost:5001/api/users/[http-method]
public class UsersController : ControllerBase
{
    private readonly DataContext _context; //Pode ser usado em toda a classe

    public UsersController(DataContext context) //injeta o DataContext qnd o UserController eh criado eh SCOPED...
    {
        _context = context;
    }

    //API ENDPOINTS
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")] //https://localhost:5001/api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
