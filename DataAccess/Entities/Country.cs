#nullable disable


using AppCore.Records.Bases;

namespace DataAccess.Entities
{
    public class Country : RecordBase
    {
        public string Name { get; set; }
        public List<City> Cities { get; set; }

        public List<UserDetail> UserDetails { get; set; }
    }
}
