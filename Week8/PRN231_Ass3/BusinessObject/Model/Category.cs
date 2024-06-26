using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Model
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required, StringLength(40)]
        public string CategoryName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}