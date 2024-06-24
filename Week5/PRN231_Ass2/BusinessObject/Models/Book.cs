using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BookId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "PublisherId is required.")]
        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher? Publisher { get; set; }

        public string? Type { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public int Price { get; set; }

        [StringLength(50, ErrorMessage = "Advance cannot exceed 50 characters.")]
        public string Advance { get; set; }

        [StringLength(50, ErrorMessage = "Loyalty cannot exceed 50 characters.")]
        public string Loyalty { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Sales must be a non-negative value.")]
        public decimal Sales { get; set; }

        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Publish Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime PublishDate { get; set; }
    }
}
