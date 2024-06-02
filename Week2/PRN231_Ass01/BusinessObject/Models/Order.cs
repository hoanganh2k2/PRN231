using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member? Member { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippedDate { get; set; }
        public int Freight { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
