using System.Security.Claims;
using API.Controller;
using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
// [ApiController]    - Passamos herdar da BaseApiController
// [Route("api/[controller]")] // https://localhost:5001/api/users/[http-method]- Passamos herdar da BaseApiController

[Authorize] //APlica pra todos metodos
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    // private readonly DataContext _context; //Pode ser usado em toda a classe
    public UsersController(IUserRepository userRepository, IMapper mapper) //injeta o DataContext qnd o UserController eh criado eh SCOPED...
    //Com o uso do Repository pattern nao temo O DataContext, "é uma classe grande e complexa".., também pra Unit Tests
    {
        _userRepository = userRepository;
        _mapper = mapper;
        // _context = context;
    }

    //API ENDPOINTS   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        // Direto no Repositoru
        // var users = await _userRepository.GetUsersAsync();
        // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

        var users = await _userRepository.GetMembersAsync();

        return Ok(users);
    }

    // [Authorize] //SO quem tem token valido pode ver essa parte
    [HttpGet("{username}")] //https://localhost:5001/api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        // var user = await _userRepository.GetUserByUsernameAsync(username);
        // var userToReturn = _mapper.Map<MemberDto>(user); --> Agora faz no Repository direto
        return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut] //O username vai ser pego do token, nao vamos passar por parametro.
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {

        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //eh oq usamos prra username No token eh NameId

        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null) return NotFound();

        //No var user o EF fica tracking o user.

        _mapper.Map(memberUpdateDto, user); //overwriting o user

        if (await _userRepository.SaveAllAsync()) return NoContent(); //204 REST

        //se nao fazer nenhuma mudanca
        return BadRequest("Failed to update user");
    }
}