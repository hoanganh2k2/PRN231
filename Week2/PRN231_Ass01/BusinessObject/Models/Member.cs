using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Members")]
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password from 6 - 30 characters")]
        public string Password { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Company Name not longer than 50 characters")]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "City not longer than 50 characters")]
        public string City { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Country not longer than 50 characters")]
        public string Country { get; set; }
    }
}
