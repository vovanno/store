using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
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

        public async Task<IdentityResult> CreateUser(CustomUser user, string password)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (password == "")
                throw new ArgumentException("Password can not be empty");
            var identityUser = new IdentityUser() { Email = user.Email, UserName = user.UserName };
            var result = await _userManager.CreateAsync(identityUser, password);
            if (!result.Succeeded) return result;
            var addedUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Role, "user"));
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Name, user.UserName));
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Email, user.Email));
            if (!user.IsPublisher) return result;
            var createdPublisher = await _unit.PublisherRepository.Add(new Publisher() { Name = user.UserName });
            await _unit.Commit();
            await _userManager.AddClaimAsync(addedUser,
                new Claim("PublisherId", createdPublisher.PublisherId.ToString()));
            return result;
        }

        public async Task<IList<Claim>> LoginUser(CustomUser user, string password)
        {
            var tempUser = await _userManager.FindByEmailAsync(user.Email);
            var result = await _userManager.CheckPasswordAsync(tempUser, password);
            var claims = await _userManager.GetClaimsAsync(tempUser);
            return result ? claims : null;
        }
    }
}
