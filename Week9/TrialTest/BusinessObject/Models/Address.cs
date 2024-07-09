using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
	public class Address
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AddressId { get; set; }
		[Required(ErrorMessage = "City is required.")]
		public string City { get; set; }
		[Required(ErrorMessage = "Address Name is required.")]
		public string AddressName { get; set; }
	}
}
