using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be no longer than 100 characters")]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public double Weight { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        [Required]
        public string ProductImage { get; set; }
    }
}
