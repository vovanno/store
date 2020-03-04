namespace Domain.Entities
{
    public class UserProfile
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsPublisher { get; set; } = false;
    }
}
