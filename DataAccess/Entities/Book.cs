#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Book : RecordBase
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int StockAmount { get; set; }
        public List<BookWriter> BookWriters { get; set; }

        [Column (TypeName = "image")]
        public byte[] Image { get; set; }

        [StringLength(5)]
        public string ImageExtension { get; set; }

    }
}
