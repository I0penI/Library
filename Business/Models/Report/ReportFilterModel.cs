#nullable disable

using System.ComponentModel;

namespace Business.Models.Report
{
    public class ReportFilterModel
    {
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Book")]
        public string BookName { get; set; }

        public List<int> WriterIds { get; set; }
    }
}
