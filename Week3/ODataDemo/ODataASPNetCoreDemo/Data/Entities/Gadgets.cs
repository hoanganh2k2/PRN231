using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataASPNetCoreDemo.Data.Entities
{
    public class Gadgets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public decimal Cost { get; set; }
        public string Type { get; set; }
    }
}
