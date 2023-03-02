#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class CartItemGroupByModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Book Name")]
        public string BookName { get; set; }

        public string TotalCount { get; set; }

        public int TotalCountValue { get; set; }

    
    }
}
