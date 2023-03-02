#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class WriterModel : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(2, ErrorMessage = "{0} must be min {1} characters!")] 
        [MaxLength(200, ErrorMessage = "{0} must be max {1} characters!")]
        [DisplayName("Writer Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(2, ErrorMessage = "{0} must be min {1} characters!")] 
        [MaxLength(200, ErrorMessage = "{0} must be max {1} characters!")]
        [DisplayName("Writer Surname")]
        public string Surname { get; set; }

        [DisplayName("Book Count")]
        public int? BookCountDisplay { get; set; }

        [DisplayName("Writer Name Surname")]
        public string NameSurnameDisplay { get; set; }
    }
}
