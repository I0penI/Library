#nullable disable


using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
	public class AccountLoginModel
	{
		[Required(ErrorMessage = "{0} is required!")]
		[MaxLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
		[MinLength(4, ErrorMessage = "{0} must be minimum {1} characters!")]
		[DisplayName("User Name")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "{0} is required!")]
		[MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
		[MinLength(4, ErrorMessage = "{0} must be minimum {1} characters!")]

		public string Password { get; set; }
	}
}
