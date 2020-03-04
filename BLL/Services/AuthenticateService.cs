using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unit;

        public AuthenticateService(UserManager<IdentityUser> userManager, IUnitOfWork unit)
        {
            _unit = unit;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(UserProfile userProfile, string password)
        {
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));
            if (password == "")
                throw new ArgumentException("Password can not be empty");
            var identityUser = new IdentityUser() { Email = userProfile.Email, UserName = userProfile.UserName };
            var result = await _userManager.CreateAsync(identityUser, password);
            if (!result.Succeeded) return result;
            var addedUser = await _userManager.FindByEmailAsync(userProfile.Email);
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Role, "userProfile"));
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Name, userProfile.UserName));
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Email, userProfile.Email));
            if (!userProfile.IsPublisher) return result;
            var createdPublisherId = await _unit.PublisherRepository.Add(new Publisher() { Name = userProfile.UserName });
            await _unit.Commit();
            await _userManager.AddClaimAsync(addedUser,
                new Claim("PublisherId", createdPublisherId.ToString()));
            return result;
        }

        public async Task<IList<Claim>> LoginUser(UserProfile userProfile, string password)
        {
            var tempUser = await _userManager.FindByEmailAsync(userProfile.Email);
            var result = await _userManager.CheckPasswordAsync(tempUser, password);
            var claims = await _userManager.GetClaimsAsync(tempUser);
            return result ? claims : null;
        }
    }
}
