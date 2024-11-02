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

namespace SyriacSources.Backend.Infrastructure.Identity;

public class IdentityUserService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IApplicationRoleService _roleService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityUserService(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IApplicationRoleService roleService,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _roleService = roleService;
        _userManager = userManager;
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
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

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
