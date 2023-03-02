#nullable disable

using System.ComponentModel;

namespace Business.Models.Report
{
    public class ReportItemModel
    {
        [DisplayName("Book Name")]
        public string BookName { get; set; }

        public string BookDescription { get; set; }

        [DisplayName("Stock Amount")]
        public string StockAmount { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
        public string WriterName { get; set; }


        #region filtreler
        public int? CategoryId { get; set; }
        public int? WriterId { get; set; }

        #endregion
    }
}
