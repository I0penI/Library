#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
    public class BookModel : RecordBase
    {
		[Required(ErrorMessage = "{0} is required!")]
		[MinLength(2, ErrorMessage = "{0} must be min {1} characters!")] 
		[MaxLength(200, ErrorMessage = "{0} must be max {1} characters!")]
		[DisplayName("Book Name")]
        public string Name { get; set; }

		[StringLength(1000, ErrorMessage = "{0} must be max {1} characters!")]
		[DisplayName("Description")]
        public string Description { get; set; }


		[ForeignKey("WriterModel")]
		[DisplayName("Writer")]
		public int? WriterId { get; set; }


		[ForeignKey("CategoryModel")]
        [Required(ErrorMessage = "{0} required!")]
		[DisplayName("Category")]
        public int? CategoryId { get; set; }


		[DisplayName("Writer")]
		public string WriterNameDisplay { get; set; }

		[DisplayName("Category")]
        public string CategoryNameDisplay { get; set; }

        [DisplayName("Stock Amount")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be 0 or bigger number!")]

        public int StockAmount { get; set; }


        [DisplayName("Writers")]
		public List<int> WriterIds { get; set; }


        public byte[] Image { get; set; }

        [StringLength(5, ErrorMessage = "{0} must be max {1} characters!")]
        public string ImageExtension { get; set; }

		[DisplayName("Image")]
		public string ImgSrcDisplay { get; set; }




	}
}
