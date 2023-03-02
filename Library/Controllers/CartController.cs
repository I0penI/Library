using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Library.Controllers
{
    public class CartController : Controller
    {
        private const string SESSIONKEY = "cart";
        private readonly IBookService _bookService;

		public CartController(IBookService bookService)
		{
			_bookService = bookService;
		}

        [HttpGet]
        public String Index()
        {
            return "deneme";
        }
        public IActionResult GetCart()
        {
            List<CartItemModel> cart = GetCartFromSession();


            List<CartItemGroupByModel> cartGroupBy = (from c in cart
                                                      group c by new { c.BookId, c.UserId, c.BookName } into cGroupBy
                                                      select new CartItemGroupByModel()
                                                      {
                                                          BookId = cGroupBy.Key.BookId,
                                                          UserId = cGroupBy.Key.UserId,
                                                          BookName = cGroupBy.Key.BookName,
                                                          TotalCount = cGroupBy.Count() + "pieces",                                                         
                                                          TotalCountValue = cGroupBy.Count(),
                                                          
                                                          
                                                      }).ToList();

                                                  return View("CartGroupBy", cartGroupBy);
            
        }

        public IActionResult AddToCart(int bookId)
        {
            List<CartItemModel> cart = GetCartFromSession();
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            BookModel book = _bookService.Query().SingleOrDefault(b => b.Id == bookId);
            if (book.StockAmount == 0)
            {
                TempData["Message"] = "Book cannot be added to cart because there is no book in stock!";
                return RedirectToAction("Index", "Books");
            }
            CartItemModel cartItem = new CartItemModel()
            {
                BookId = bookId,
                BookName = book.Name,
                UserId = userId
            };
            cart.Add(cartItem);
            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SESSIONKEY, cartJson);
            TempData["Message"] = $"{book.Name} added to cart.";
            return RedirectToAction("Index", "Books");

        }

        private List<CartItemModel> GetCartFromSession()
        {
            List<CartItemModel> cart = new List<CartItemModel>();
            string cartJson = HttpContext.Session.GetString("cart");
            if (!string.IsNullOrWhiteSpace(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<CartItemModel>>(cartJson);
                int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                cart = cart.Where(c => c.UserId == userId).ToList();
            }
            return cart;

        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("cart");


            List<CartItemModel> cart = GetCartFromSession();
            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SESSIONKEY, cartJson);

            TempData["Message"] = "Cart cleared";
            return RedirectToAction(nameof(GetCart));

        }

        public IActionResult DeleteFromCart(int bookId, int userId)
        {
            List<CartItemModel> cart = GetCartFromSession();
            CartItemModel cartItem = cart.FirstOrDefault(c => c.BookId == bookId && c.UserId == userId);
            if (cartItem is not null)
                cart.Remove(cartItem);

            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SESSIONKEY, cartJson);

            TempData["Message"] = "Item removed from cart.";
            return RedirectToAction(nameof(GetCart));
        }
    }
}
