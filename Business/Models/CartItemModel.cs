#nullable disable

using System.ComponentModel;


namespace Business.Models
{
    public class CartItemModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Book Name")]
        public string BookName { get; set; }
    }
}
