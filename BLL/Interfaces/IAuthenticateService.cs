using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IAuthenticateService
    {
        Task<IdentityResult> CreateUser(CustomUser user, string password);
        Task<IList<Claim>> LoginUser(CustomUser user, string password);
    }
}
