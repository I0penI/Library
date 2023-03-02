#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel;

namespace DataAccess.Entities
{
    public class Writer : RecordBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        [DisplayName("Birthday")]
        public DateTime? DateTime { get; set; }
       
        public List<BookWriter> BookWriter { get; set; }
    }
}
