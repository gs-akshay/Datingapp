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

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
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

if(user==null)
{
    return NotFound();
}

return user;
}
}
