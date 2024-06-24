using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email must be between 1 and 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name must be between 1 and 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name must be between 1 and 50 characters.")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Middle Name cannot exceed 50 characters.")]
        public string MiddleName { get; set; }

        [StringLength(100, ErrorMessage = "Source cannot exceed 100 characters.")]
        public string Source { get; set; }
        public int PublisherId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher? Publisher { get; set; }


    }
}
