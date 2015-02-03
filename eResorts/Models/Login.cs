using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter your email address."), RegularExpression(@"(?i)\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Invalid email address entered.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password."), MinLength(6, ErrorMessage = "The password has a minimum length of 6 characters.")]
        public string Password { get; set; }

        [Display(Name = "Stay logged in.")]
        public bool KeepMeSignedIn { get; set; }

        //[Required(ErrorMessage = "Please select the company")]
        //[Display(Name = "Company")]
        //public int Company { get; set; }
    }
}