using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using OnlineStoreApi.AuthApi;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var existedUser = await _userManager.FindByEmailAsync(userProfile.Email);
            if(existedUser != null)
                throw new Exception($"User with email: {userProfile.Email} already exist");

            var identityUser = new IdentityUser() { Email = userProfile.Email, UserName = userProfile.UserName };
            var result = await _userManager.CreateAsync(identityUser, password);
            if (!result.Succeeded) return result;
            var addedUser = await _userManager.FindByEmailAsync(userProfile.Email);
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Role, "userProfile"));
            await _userManager.AddClaimAsync(addedUser, new Claim("Name", userProfile.UserName));
            await _userManager.AddClaimAsync(addedUser, new Claim(ClaimTypes.Email, userProfile.Email));
            await _userManager.AddClaimAsync(addedUser, new Claim("FirstName", userProfile.FirstName));
            await _userManager.AddClaimAsync(addedUser, new Claim("LastName", userProfile.LastName));
            await _userManager.AddClaimAsync(addedUser, new Claim("City", userProfile.City));
            await _userManager.AddClaimAsync(addedUser, new Claim("Phone", userProfile.Phone));
            if (!userProfile.IsPublisher) return result;
            var createdPublisherId = await _unit.ManufacturerRepository.Add(new Manufacturer() { Name = userProfile.UserName });
            await _unit.Commit();
            await _userManager.AddClaimAsync(addedUser,
                new Claim("ManufacturerId", createdPublisherId.ToString()));
            return result;
        }

        public async Task<IEnumerable<Claim>> UpdateUserProfile(string userId, UpdateUserProfileRequest request)
        {
            var existedUser = await _userManager.FindByEmailAsync(userId);
            if (existedUser == null)
                throw new Exception($"User with email: {userId} was not found");

            var claims = await _userManager.GetClaimsAsync(existedUser);

            if(request.FirstName != claims.Single(p => p.Type == "FirstName").Value)
                await _userManager.ReplaceClaimAsync(existedUser, claims.Single(p => p.Type == "FirstName"), new Claim("FirstName", request.FirstName));

            if (request.FirstName != claims.Single(p => p.Type == "LastName").Value)
                await _userManager.ReplaceClaimAsync(existedUser, claims.Single(p => p.Type == "LastName"), new Claim("LastName", request.LastName));

            if (request.FirstName != claims.Single(p => p.Type == "Phone").Value)
                await _userManager.ReplaceClaimAsync(existedUser, claims.Single(p => p.Type == "Phone"), new Claim("Phone", request.Phone));

            if (request.FirstName != claims.Single(p => p.Type == "City").Value)
                await _userManager.ReplaceClaimAsync(existedUser, claims.Single(p => p.Type == "City"), new Claim("City", request.City));

            var updatedClaims = await _userManager.GetClaimsAsync(existedUser);
            return updatedClaims;
        }

        public async Task<IList<Claim>> LoginUser(UserProfile userProfile, string password)
        {
            var tempUser = await _userManager.FindByEmailAsync(userProfile.Email);
            if(tempUser == null)
                throw new UnauthorizedAccessException($"User with email: {userProfile.Email} was not found");
            var result = await _userManager.CheckPasswordAsync(tempUser, password);
            var claims = await _userManager.GetClaimsAsync(tempUser);
            return result ? claims : null;
        }
    }
}
