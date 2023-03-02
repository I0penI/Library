using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using System.Diagnostics.Metrics;

namespace Business.Services
{
    public interface IBookService : IService<BookModel>
    {
        Result DeleteImage(int id);
    }
    public class BookService : IBookService
    {
        private readonly BookRepoBase _bookRepo;

        public BookService(BookRepoBase bookRepo)
        {
            _bookRepo = bookRepo;
        }
        public Result Add(BookModel model)
        {
            if (_bookRepo.Exists(b => b.Name.ToLower() == model.Name.Trim()))
                return new ErrorResult("Book with same name exists!");


            Book entity = new Book()
            {

                Description = model.Description?.Trim(),
                Name = model.Name.Trim(),
                StockAmount=model.StockAmount,

                CategoryId = model.CategoryId.Value,
                Image = model.Image,
                ImageExtension = model.ImageExtension?.ToLower(),


                BookWriters = model.WriterIds?.Select(wId => new BookWriter
                {
                    WriterId = wId,
                }).ToList()


            };
            _bookRepo.Add(entity);
            return new SuccessResult("Book added successfully");
        }


        public Result Delete(int id)
        {
            _bookRepo.Delete<BookWriter>(bw => bw.BookId == id);
            _bookRepo.Delete(id);
            return new SuccessResult("Book  deleted successfully.");
        }



        public Result DeleteImage(int id)
        {
            var entity = _bookRepo.Query(i => i.Id == id).SingleOrDefault();
            entity.Image = null;
            entity.ImageExtension = null;
            _bookRepo.Update(entity);
            return new SuccessResult("Product image deleted succesfully");
        }


        public void Dispose()
        {
            _bookRepo.Dispose();
        }

        public IQueryable<BookModel> Query()
        {
            return _bookRepo.Query(b => b.Category).Select(b => new BookModel()
            {
                Description = b.Description,
                Name = b.Name,

                Id = b.Id,

                CategoryNameDisplay = b.Category.Name,
                CategoryId = b.CategoryId,

                WriterIds = b.BookWriters.Select(bw => bw.WriterId).ToList(),

                WriterNameDisplay = string.Join("<br />", b.BookWriters.Select(bw => bw.Writer.Name + bw.Writer.Surname)),
                StockAmount=b.StockAmount,

                Image = b.Image,
                ImageExtension = b.ImageExtension,
                ImgSrcDisplay = b.Image != null ?
                    (
                        b.ImageExtension == ".jpg" || b.ImageExtension == ".jpeg" ? "data:image/jpeg;base64," : "data:image/png;base64,"

                    ) + Convert.ToBase64String(b.Image) : null


            });
        }

        public Result Update(BookModel model)
        {
            if (_bookRepo.Exists(b => b.Name.ToLower() == model.Name.ToLower().Trim() && b.Id != model.Id))
                return new ErrorResult("Book with same name exists!");

            Book entity = _bookRepo.Query().SingleOrDefault(b => b.Id == model.Id);


            entity.Id = model.Id;
            entity.Name = model.Name.Trim();
            entity.CategoryId = model.CategoryId.Value;
            entity.Description = model.Description?.Trim();
            entity.StockAmount = model.StockAmount;

            entity.BookWriters = model.WriterIds?.Select(wId => new BookWriter()
            {
                WriterId = wId,
            }).ToList();

            if (model.Image != null)
            {
                entity.Image = model.Image;
                entity.ImageExtension = model.ImageExtension.ToLower(); 
            }
            _bookRepo.Update(entity);
            return new SuccessResult("Book updated successfully");

        }
    }
}
