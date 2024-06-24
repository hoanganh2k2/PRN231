using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public class BookAuthor
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }

        [Key, Column(Order = 1)]
        public int AuthorId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author? Author { get; set; }

        public int AuthorOrder { get; set; }
        public int RoyaltyPercentage { get; set; }
    }
}
