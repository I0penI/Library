using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	[Area("Account")]  
	public class UserController : Controller
	{
		private readonly IAccountService _accountService;


		public UserController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
