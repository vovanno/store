using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        Task ChangeRole(string userId, string role);
        Task DeleteUser(string id);
        Task EditUser(string id, IdentityUser newUser);
    }
}
