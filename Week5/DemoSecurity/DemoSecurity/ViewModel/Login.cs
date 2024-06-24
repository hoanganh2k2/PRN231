using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DemoSecurity.ViewModel
{
    public class Login : IdentityUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
