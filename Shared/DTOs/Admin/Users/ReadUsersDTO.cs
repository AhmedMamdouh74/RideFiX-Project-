namespace SharedData.DTOs.Admin.Users
{
    public class ReadUsersDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
       
        public bool IsActivated { get; set; }
        public int? Rate {  get; set; }
        public string? Image {  get; set; }
    }
}
