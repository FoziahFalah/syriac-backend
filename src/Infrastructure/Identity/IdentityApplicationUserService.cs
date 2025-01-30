using SyriacSources.Backend.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using SyriacSources.Backend.Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace SyriacSources.Backend.Infrastructure.Identity;

public class IdentityApplicationUserService : IIdentityApplicationUserService
{
    private readonly UserManager<IdentityApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IApplicationRoleService _roleService;
    private readonly IUserClaimsPrincipalFactory<IdentityApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityApplicationUserService(
        UserManager<IdentityApplicationUser> userManager,
        IMapper mapper,
        IHttpContextAccessor contextAccessor,
        IApplicationRoleService roleService,
        IUserClaimsPrincipalFactory<IdentityApplicationUser> userClaimsPrincipalFactory,
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
    //public async Task<IdentityApplicationUser?> GetIdentityUserAsync(string email)
    //{
    //    var entity = (await _userManager.FindByEmailAsync(email));
    //    if(entity == null)
    //    {
    //        return null;
    //    }
    //    IdentityApplicationUser user = new IdentityApplicationUser
    //    {
    //        Email = email,
    //        Id = entity.Id,
    //    };

    //    return user;
    //}
    
    public async Task<bool> EmailExists(string email){
        return await _userManager.FindByEmailAsync(email) != null;
    }
    public async Task<int> CreateUserLoginAsync(string email, string password, int userId)
    {
        var username = email.Split("@")[1];
        var user = new IdentityApplicationUser
        {
            Id = userId,
            UserName = username,
            NormalizedUserName = username.Normalize(),
            Email = email,
            NormalizedEmail = email.Normalize(),
        };

        var result = await _userManager.CreateAsync(user, password);

        return (user.Id);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {

        //var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

        //if (user == null || !(await _userManager.GetClaimsAsync(user)).Any(i => i.Type == po && i.Value == value))
        //{
        //    return Result.Failure(new[] { $"ApplicationUser does not have claim for \"{type}\"." });
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

    public async Task<Result> AuthenticateAsync(string email, string password) 
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user == null)
            return (Result.Failure(new List<string> {"ApplicationUser not Found"}));

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            var error = new List<string> { "Email or Password is incorrect" };
            return (Result.Failure(error));
        }

        return (Result.Success(user.Id));
    }
    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success(userId);
    }

    public async Task<Result> DeleteUserAsync(IdentityApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

}
