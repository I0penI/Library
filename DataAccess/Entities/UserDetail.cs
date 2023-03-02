#nullable disable

using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace DataAccess.Entities
{
    public class UserDetail
    {
        [Key]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [StringLength(150)]
        public string  Email { get; set; }

        [Required]
        [StringLength(3000)]
        public string Address { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public Sex Sex { get; set; }

    }
}
