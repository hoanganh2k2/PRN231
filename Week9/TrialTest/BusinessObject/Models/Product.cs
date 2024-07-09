using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
	public class Product
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; }
		[Required(ErrorMessage = "Product Name is required.")]
		public string ProductName { get; set; }
		[Required(ErrorMessage = "Description is required.")]
		public string Description { get; set; }
		[Required(ErrorMessage = "Product Price is required.")]
		[Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
		public int Price { get; set; }
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual User? User { get; set; }
	}
}
