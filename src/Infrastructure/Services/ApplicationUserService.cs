using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Common;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Data;
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Domain.Constants;
using System.Data;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationUserService : IApplicationUserService
{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationUserService> _logger;

    public ApplicationUserService(ApplicationDbContext context, ILogger<ApplicationUserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    //public Task<(Result, Contributor)> GetUserById(int id, CancellationToken cancellationToken)
    //{
    //    return _context.Contributors.Where(x => x.)
    //}

    public async Task<Contributor?> GetUserByEmail(string emailAddress, CancellationToken cancellationToken)
    {
        Contributor? entity = await _context.Contributors.SingleOrDefaultAsync(x => x.EmailAddress == emailAddress);
        return entity;
    }


    public async Task<(Result, int)> CreateUser(Contributor contributor, CancellationToken cancellationToken)
    {
        int result = 0;

        try{
            _context.Contributors.Add(contributor);
            result = await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
        }

        return ( result.ToApplicationResult() , result);
    }

    public async Task<(Result, int)> DeleteUser(Contributor contributor, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.Contributors.Remove(contributor);
            result = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return (result.ToApplicationResult(), result);
    }


    public async Task<(Result, int)> UpdateUser(Contributor contributor, CancellationToken cancellationToken)
    {
        int result = 0;
        try
        {
            _context.Contributors.Update(contributor);
            result = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return (result.ToApplicationResult(), result);
    }
}
