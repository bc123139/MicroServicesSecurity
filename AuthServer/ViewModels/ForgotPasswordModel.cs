using System.ComponentModel.DataAnnotations;

namespace AuthServer.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
