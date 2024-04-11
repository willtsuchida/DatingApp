using System.Security.Claims;
using API.Controller;
using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize] 
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UsersController(IUserRepository userRepository, IMapper mapper)   
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    //API ENDPOINTS   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();

        return Ok(users);
    }


    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut] 
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {

        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null) return NotFound();

        _mapper.Map(memberUpdateDto, user); //overwriting o user

        if (await _userRepository.SaveAllAsync()) return NoContent(); //204 REST

        return BadRequest("Failed to update user");
    }
}