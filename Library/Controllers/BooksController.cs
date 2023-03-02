using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using Business.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IWriterService _writerService;
        
        public BooksController(IBookService bookService, ICategoryService categoryService, IWriterService writerService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _writerService = writerService;
        }
        public IActionResult Index()
        {
            var books = _bookService.Query().ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.Query().ToList(),"Id", "Name");
			ViewBag.Writers = new MultiSelectList(_writerService.Query().ToList(), "Id", "NameSurnameDisplay");
			return View();
        }
        [HttpPost]
        public IActionResult Create(BookModel book , IFormFile? uploadedImage)
        {
            if (ModelState.IsValid)
            {
                Result result;
                result=UpdateImage(book,uploadedImage);
                
                if (result.IsSuccessful)
                {
                    result = _bookService.Add(book);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                }
                ViewData["Message"] = result.Message; 
            }
            ViewBag.Categories = new SelectList(_categoryService.Query().ToList(), "Id", "Name", book.CategoryId);
            ViewBag.Writers = new SelectList(_writerService.Query().ToList(), "Id", "Name", book.WriterIds);
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _bookService.Query().SingleOrDefault(b => b.Id == id);
            if (book is null)
            {
                return View("book not found!");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.Query().ToList(), "Id", "Name", book.CategoryId);
            ViewBag.Writers = new MultiSelectList(_writerService.Query().ToList(), "Id", "NameSurnameDisplay", book.WriterIds);
            return View(book);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(BookModel book , IFormFile? uploadedImage)
        {
            if (ModelState.IsValid)
            {
                Result result = UpdateImage(book, uploadedImage);
                if (result.IsSuccessful)
                {
                    result = _bookService.Update(book);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError("", result.Message); // Error
            }
            ViewBag.categoryId = new SelectList(_categoryService.Query().ToList(), "Id", "Name", book.CategoryId);
            ViewBag.Writers = new MultiSelectList(_writerService.Query().ToList(), "Id", "NameSurnameDisplay", book.WriterIds);
            return View(book);
        }

        public IActionResult Details (int id)
        {
            var book = _bookService.Query().SingleOrDefault(b => b.Id == id);
            if (book is null)
                return View("Book not found!");
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _bookService .Query().SingleOrDefault(s => s.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
        {
            var result = _bookService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));

        }
        public IActionResult DeleteImage(int id)
        {
            var result =_bookService.DeleteImage(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Details), new { id = id });
        }
        private Result UpdateImage(BookModel resultModel, IFormFile uploadedImage)
        {
            Result result = new SuccessResult();

            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                string uploadedFileName = uploadedImage.FileName;
                string uploadedFileExtension = Path.GetExtension(uploadedFileName);

                if (!AppSettings.AcceptedExtensions.Split(',').Any(ae => ae.ToLower
                ().Trim() == uploadedFileExtension.ToLower()))// yüklediğimiz dosya uzantısı bizde varmı?
                    result = new ErrorResult($"Image cant be uploaded because accepted extensions are {AppSettings.AcceptedExtensions}!");
                if (result.IsSuccessful)
                {
                    double acceptedFileLength = AppSettings.AcceptedLength;
                    double acceptedFileLengthInBytes = acceptedFileLength * Math.Pow(1024, 2);

                    if (uploadedImage.Length > acceptedFileLengthInBytes)
                    {
                        result = new ErrorResult("Image cant be uploaded because acceğted file size is" + AppSettings.AcceptedLength.ToString
                            ("N1") + "!");
                    }
                    if (result.IsSuccessful)
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            uploadedImage.CopyTo(memoryStream); 
                            resultModel.Image = memoryStream.ToArray();
                            resultModel.ImageExtension = uploadedFileExtension; 

                        }
                    }
                }

            }
            return result;
        }
       
    }
}



        

