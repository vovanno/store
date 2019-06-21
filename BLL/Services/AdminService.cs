using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ChangeRole(string userId, string role)
        {
            if (userId == "")
                throw new ArgumentException("User id can not be empty.");
            if (userId == "")
                throw new ArgumentException("Role can not be empty.");
            var roles = new List<string>
            {
                "administrator",
                "manager",
                "publisher",
                "moderator"
            };
            if (!role.Contains(role))
                throw new ArgumentException("Such role does not exist, please choose role from the list.");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new EntryNotFoundException($"User with id = {userId} does not found.");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
        }

        public async Task DeleteUser(string id)
        {
            if (id == "")
                throw new ArgumentException("User id can not be empty.");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return;
            await _userManager.DeleteAsync(user);
        }

        public async Task EditUser(string id, IdentityUser newUser)
        {
            if (id != newUser.Id)
                throw new ArgumentException("User id and edited user does not match");

            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser));
            var oldUser = await _userManager.FindByIdAsync(id);
            if (oldUser == null)
                return;
            await _userManager.UpdateAsync(newUser);
        }
    }
}
