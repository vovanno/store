using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IAuthenticateService
    {
        Task<IdentityResult> CreateUser(UserProfile userProfile, string password);
        Task<IList<Claim>> LoginUser(UserProfile userProfile, string password);
    }
}
