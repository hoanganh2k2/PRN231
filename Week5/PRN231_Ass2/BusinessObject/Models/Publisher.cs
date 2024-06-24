using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public class Publisher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Publisher Name is required.")]
        [StringLength(100, ErrorMessage = "Publisher Name must be between 1 and 100 characters.")]
        public string PublisherName { get; set; }

        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; }

        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        public string State { get; set; }
    }
}
