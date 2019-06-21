using Microsoft.AspNetCore.Identity;

namespace BLL.DTO
{
    public class CustomUser
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsPublisher { get; set; } = false;
    }
}
