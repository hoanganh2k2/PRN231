using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [Key]
        [Column(Order = 2)]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order? Order { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
    }
}
