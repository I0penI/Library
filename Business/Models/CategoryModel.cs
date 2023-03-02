#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class CategoryModel : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }



        #region Sayfada ihtiyacımız olan özellikler
        [DisplayName("Book Count")]
        public int? BookCountDisplay { get; set; }
        #endregion
    }
}
