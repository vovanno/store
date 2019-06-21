using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPut]
        [Route("{id}/role")]
        public async Task<IActionResult> ChangeRole(string id, string role)
        {
            await _adminService.ChangeRole(id, role);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/user")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _adminService.DeleteUser(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{id}/user")]
        public async Task<IActionResult> EditUser(string id, IdentityUser user)
        {
            await _adminService.EditUser(id, user);
            return Ok();
        }
    }
}