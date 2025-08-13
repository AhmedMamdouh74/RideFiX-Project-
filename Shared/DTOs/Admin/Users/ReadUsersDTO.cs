namespace SharedData.DTOs.Admin.Users
{
    public class ReadUsersDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public bool IsActivated { get; set; }
    }
}
