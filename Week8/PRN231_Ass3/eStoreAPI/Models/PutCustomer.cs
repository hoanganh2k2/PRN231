using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.Models
{
    public class PutCustomer
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Password { get; set; }
    }
}
