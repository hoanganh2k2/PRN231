using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
	public class User
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }
		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }
		[Required(ErrorMessage = "AddressId is required.")]
		public int AddressId { get; set; }
		[ForeignKey("AddressId")]
		public virtual Address? Address { get; set; }
	}
}
