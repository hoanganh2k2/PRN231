using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role desciption is required.")]
        [StringLength(100, ErrorMessage = "Role desciption must be between 1 and 100 characters.")]
        public string RoleDesc { get; set; }
    }
}
