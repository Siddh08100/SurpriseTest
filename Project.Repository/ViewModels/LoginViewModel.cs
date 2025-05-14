using System.ComponentModel.DataAnnotations;

namespace Project.Repository.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", ErrorMessage = "Enter a Valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    public bool RememberMe {get;set;}
}
