using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Library.Controllers
{
    

    public class WritersController : Controller
    {
        private readonly IWriterService _writerService;

        public WritersController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public IActionResult Index()
        {
            List<WriterModel> writerList = _writerService.Query().ToList();
            return View(writerList);
        }

        public IActionResult Details(int id)
        {
            WriterModel writer = _writerService.Query().SingleOrDefault(w => w.Id == id);
            if (writer == null)
            {
                return View("Error", "Writer not found!");
            }
            return View(writer);
        }
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WriterModel writer)
        {
            if (ModelState.IsValid)
            {
                var result = _writerService.Add(writer);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(writer);
        }

        public IActionResult Edit(int id)
        {
            WriterModel writer = _writerService.Query().SingleOrDefault(w => w.Id == id);
            if (writer == null)
            {
                return View("Error", "Writer not found!");
            }
            return View(writer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WriterModel writer)
        {
            if (ModelState.IsValid)
            {
                var result = _writerService.Update(writer);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(writer);
        }

        public IActionResult Delete(int id)
        {
            WriterModel writer = _writerService.Query().SingleOrDefault(w => w.Id == id);
            if (writer == null)
            {
                return View("Error", "Writer not found!");
            }
            return View(writer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _writerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
