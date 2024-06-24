using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcServiceDemo.DataAccess
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
    }
}
