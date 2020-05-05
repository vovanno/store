
namespace OnlineStoreApi.AuthApi
{
    public class RegisterUserRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }

    public class LoginUserRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
