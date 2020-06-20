
namespace OnlineStoreApi.AuthApi
{
    public class RegisterUserRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }

    public class UpdateUserProfileRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

    }

    public class LoginUserRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
