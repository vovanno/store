namespace Domain.Entities
{
    public class UserProfile
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public bool IsPublisher { get; set; } = false;
    }
}
