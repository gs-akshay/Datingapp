using System;
using System.Net.Mime;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using AutoMapper;
using System.ComponentModel;
using API.DTOs;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository,IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    [HttpGet("{username}")]  //api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userRepository.GetMemberAsync(username);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(username==null) return BadRequest("No username found in token");

        var user= await userRepository.GetUserByUsernameAsync(username);

        if(user==null) return BadRequest("Could not find user");

        mapper.Map(memberUpdateDto,user);

        if(await userRepository.SaveAllASync()) return NoContent();

        return BadRequest("failed to update the user");
    }
}