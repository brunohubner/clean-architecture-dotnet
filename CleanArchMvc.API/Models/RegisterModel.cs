using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid format email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, ErrorMessage = "The {0} must be least {2} and at max "
        + "{1} characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; }
}