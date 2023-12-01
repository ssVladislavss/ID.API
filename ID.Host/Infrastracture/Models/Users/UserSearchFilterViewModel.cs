namespace ID.Host.Infrastracture.Models.Users
{
    public class UserSearchFilterViewModel
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Role { get; set; }
    }
}
