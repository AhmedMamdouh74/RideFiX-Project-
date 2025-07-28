using System;
using System.ComponentModel.DataAnnotations;

namespace SharedData.DTOs.Account
{
    public class RegisterStep1Dto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "SSN is required")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "SSN must be exactly 14 digits")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name must be less than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("^(CarOwner|Technician)$", ErrorMessage = "Role must be either 'CarOwner' or 'Technician'")]
        public string Role { get; set; }

        [Required(ErrorMessage = "PIN is required")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "PIN must be between 4 and 6 digits")]
        public int PIN { get; set; }

        public TimeOnly? StartWorking { get; set; }

        public TimeOnly? EndWorking { get; set; }

        [StringLength(300, ErrorMessage = "Description must be less than 300 characters")]
        public string? Description { get; set; }
    }
}
    