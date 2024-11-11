using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.User;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace SyriacSources.Backend.Infrastructure.Identity;

public class IdentityUserService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IApplicationRoleService _roleService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityUserService(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IHttpContextAccessor contextAccessor,
        IApplicationRoleService roleService,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _roleService = roleService;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }
    public async Task<ApplicationUserDto?> GetUserAsync(string email)
    {
        var entity = await _userManager.FindByEmailAsync(email);
        if(entity == null)
        {
            return null;
        }
        ApplicationUserDto user = new ApplicationUserDto
        {
            EmailAddress = email,
            Id = entity.Id
        };

        return _mapper.Map<ApplicationUserDto>(user);
    }
    
    public async Task<bool> EmailExists(string email){
        return await _userManager.FindByEmailAsync(email) != null;
    }
    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id.ToString());
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {

        //var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

        //if (user == null || !(await _userManager.GetClaimsAsync(user)).Any(i => i.Type == po && i.Value == value))
        //{
        //    return Result.Failure(new[] { $"User does not have claim for \"{type}\"." });
        //}

        //return result.Succeeded;



        var user = await _userManager.FindByIdAsync(userId);
        //var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id.ToString() == userId);
        var principal = _contextAccessor.HttpContext?.User;
        
        if (user == null || principal == null)
        {
            return false;
        }
        //var principal = httpContext.Principal;
        //var claims = await _userManager.Users..GetClaimsAsync(user);
        //var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        // Debug: Print all claims to check if policies claim is included
        var policiesClaim = principal.Claims
            .Where(c => c.Type == CustomClaimTypes.Permission)
            .Select(c => c.Value)
            .ToList();

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<(Result Result,int Id)> AuthenticateAsync(string email, string password) 
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user == null)
            return (Result.Failure(new List<string> {"User not Found"}), 0 );

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            var error = new List<string> { "Email or Password is incorrect" };
            return (Result.Failure(error),0);
        }

        return (Result.Success(), user.Id);
    }
    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

}
