﻿using API.Controller;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class LikesController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly ILikesRepository _likesRepository;
    public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
    {
        _likesRepository = likesRepository;
        _userRepository = userRepository;
    }

    [HttpPost("{username}")] //username of the the person try are about to like as a route parameter
    //This post method will be not creating anything on server but updating the join Like Table
    public async Task<ActionResult> AddLike(string username)
    {
        var sourceUserId = User.GetUserId(); //getting Id from logged in user
        var likedUser = await _userRepository.GetUserByUsernameAsync(username); //Getting user whos being liked
        var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId); //Gets the user whos liking/logged

        if (likedUser == null) return NotFound();

        if (sourceUser.UserName == username) return BadRequest("You cannot like yourself");

        var userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

        if (userLike != null) return BadRequest("You already liked this user");

        userLike = new UserLike //Tabela relacional
        {
            SourceUserId = sourceUserId,
            TargetUserId = likedUser.Id,
        };

        sourceUser.LikedUsers.Add(userLike);//Adiciona

        if (await _userRepository.SaveAllAsync()) return Ok();

        return BadRequest("Failed to like user");
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
    {
        likesParams.UserId = User.GetUserId();

        var users = await _likesRepository.GetUserLikes(likesParams);

        Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

        return Ok(users);
    }
}
