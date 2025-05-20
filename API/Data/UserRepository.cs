using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context,IMapper mapper) : IUserRepository
{
    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
                   return await context.Users
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .ToListAsync();
    }

    public async Task<IEnumerable<AppUser>> GetUserAsync()
    {
        return await context.Users.Include(x=>x.Photos).ToListAsync();
    }

    public  async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.Include(x=>x.Photos).SingleOrDefaultAsync(x=>x.UserName==username);
    }

public async Task<bool> SaveAllASync()
{
    try
    {
        return await context.SaveChangesAsync() > 0;
    }
    catch (DbUpdateException ex)
    {
        Console.WriteLine("DbUpdateException:");
        Console.WriteLine("Message: " + ex.Message);
        Console.WriteLine("InnerException: " + ex.InnerException?.Message);

        // Optional: Log stack trace if needed
        Console.WriteLine("StackTrace: " + ex.StackTrace);

        throw; // Let the middleware catch it and return 500
    }
}

    public void Update(AppUser user)
    {
    context.Entry(user).State=EntityState.Modified;
    }
}
