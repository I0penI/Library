#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	public class BookWriter
	{
		[Key]
		[Column(Order = 0)]
		public int BookId { get; set; }
		public Book Book { get; set; } 
		
		[Key]
		[Column(Order = 1)]
		public int WriterId { get; set; }
		public Writer Writer { get; set; } 
		
	}
}
